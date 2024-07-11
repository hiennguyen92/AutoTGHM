using System;
using System.Drawing;
using System.Drawing.Imaging;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;

using ImageProgressing.Interfaces;

using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;

namespace ImageProgressing
{
    public class DxWindow : IDisposable
    {
        private readonly ICaptureMethod _captureMethod;
        private readonly string _title;

        public DxWindow(string title, ICaptureMethod captureMethod)
        {
            _title = title;
            _captureMethod = captureMethod;
        }

        public void Dispose()
        {
            _captureMethod?.Dispose();
        }

        public void Show()
        {
            using var form = new RenderForm(_title);

            // create a Device and SwapChain
            var swapChainDescription = new SwapChainDescription
            {
                BufferCount = 2,
                Flags = SwapChainFlags.None,
                IsWindowed = true,
                ModeDescription = new ModeDescription(form.ClientSize.Width, form.ClientSize.Height, new Rational(60, 1), Format.B8G8R8A8_UNorm),
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, swapChainDescription, out var device, out var swapChain);
            using var swapChain1 = swapChain.QueryInterface<SwapChain1>();

            // ignore all Windows events
            using var factory = swapChain1.GetParent<Factory>();
            factory.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);

            using var vertexShaderByteCode = ShaderBytecode.CompileFromFile("./Shader.fx", "VS", "vs_5_0");
            using var vertexShader = new VertexShader(device, vertexShaderByteCode);

            using var pixelShaderByteCode = ShaderBytecode.CompileFromFile("./Shader.fx", "PS", "ps_5_0");
            using var pixelShader = new PixelShader(device, pixelShaderByteCode);

            using var layout = new InputLayout(device, vertexShaderByteCode, new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                new InputElement("TEXCOORD", 0, Format.R32G32_Float, 12, 0)
            });

            using var vertexes = Buffer.Create(device, BindFlags.VertexBuffer, new[]
            {
                new Vertex { Position = new RawVector3(-1.0f, 1.0f, 0.5f), TexCoord = new RawVector2(0.0f, 0.0f) },
                new Vertex { Position = new RawVector3(1.0f, 1.0f, 0.5f), TexCoord = new RawVector2(1.0f, 0.0f) },
                new Vertex { Position = new RawVector3(-1.0f, -1.0f, 0.5f), TexCoord = new RawVector2(0.0f, 1.0f) },
                new Vertex { Position = new RawVector3(1.0f, -1.0f, 0.5f), TexCoord = new RawVector2(1.0f, 1.0f) }
            });

            var samplerStateDescription = new SamplerStateDescription
            {
                AddressU = TextureAddressMode.Wrap,
                AddressV = TextureAddressMode.Wrap,
                AddressW = TextureAddressMode.Wrap,
                Filter = Filter.MinMagMipLinear
            };

            device.ImmediateContext.InputAssembler.InputLayout = layout;
            device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;
            device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexes, Utilities.SizeOf<Vertex>(), 0));
            device.ImmediateContext.VertexShader.Set(vertexShader);
            device.ImmediateContext.PixelShader.SetSampler(0, new SamplerState(device, samplerStateDescription));
            device.ImmediateContext.PixelShader.Set(pixelShader);

            // create a first views
            var backBuffer = Resource.FromSwapChain<Texture2D>(swapChain1, 0);
            var renderView = new RenderTargetView(device, backBuffer);

            device.ImmediateContext.Rasterizer.SetViewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);
            device.ImmediateContext.OutputMerger.SetTargets(renderView);

            // listen events (but processed in render loop)
            var isResized = false;
            form.UserResized += (_, __) => isResized = true;

            RenderLoop.Run(form, () =>
            {
                // ReSharper disable AccessToDisposedClosure
                if (!_captureMethod.IsCapturing)
                    _captureMethod.StartCapture(form.Handle, device, factory);

                if (isResized)
                {
                    Utilities.Dispose(ref backBuffer);
                    Utilities.Dispose(ref renderView);

                    swapChain1.ResizeBuffers(swapChainDescription.BufferCount, form.ClientSize.Width, form.ClientSize.Height, Format.Unknown, SwapChainFlags.None);
                    backBuffer = Resource.FromSwapChain<Texture2D>(swapChain1, 0);
                    renderView = new RenderTargetView(device, backBuffer);

                    device.ImmediateContext.Rasterizer.SetViewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);
                    device.ImmediateContext.OutputMerger.SetTargets(renderView);

                    isResized = false;
                }

                // clear view
                device.ImmediateContext.ClearRenderTargetView(renderView, new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));

                using var texture2d = _captureMethod.TryGetNextFrameAsTexture2D(device);
                if (texture2d != null)
                {
                    SaveTexture2DToImage(device, texture2d, "out.png");
                    using var shaderResourceView = new ShaderResourceView(device, texture2d);
                    device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView);
                }


                // draw it
                device.ImmediateContext.Draw(4, 0);
                swapChain1.Present(1, PresentFlags.None, new PresentParameters());

                // ReSharper restore AccessToDisposedClosure
            });





            renderView.Dispose();
            backBuffer.Dispose();
            swapChain.Dispose();
            device.Dispose();
        }


        public static void SaveTexture2DToImage(Device device, Texture2D texture, string filePath)
        {
            // Lấy mô tả của texture
            var description = texture.Description;

            // Tạo một texture staging để sao chép dữ liệu từ GPU sang CPU
            var stagingDescription = new Texture2DDescription
            {
                Width = description.Width,
                Height = description.Height,
                MipLevels = 1,
                ArraySize = 1,
                Format = description.Format,
                Usage = ResourceUsage.Staging,
                SampleDescription = new SampleDescription(1, 0),
                BindFlags = BindFlags.None,
                CpuAccessFlags = CpuAccessFlags.Read,
                OptionFlags = ResourceOptionFlags.None
            };

            using (var stagingTexture = new Texture2D(device, stagingDescription))
            {
                // Sao chép dữ liệu từ texture gốc sang staging texture
                device.ImmediateContext.CopyResource(texture, stagingTexture);

                // Lấy dữ liệu từ staging texture
                var dataBox = device.ImmediateContext.MapSubresource(stagingTexture, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.None);
                var dataStream = dataBox.DataPointer;
                var dataRowPitch = dataBox.RowPitch;

                // Tạo Bitmap từ dữ liệu
                var bitmap = new Bitmap(description.Width, description.Height, PixelFormat.Format32bppArgb);

                // Khóa các bit của bitmap để có thể truy cập vào vùng nhớ
                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, description.Width, description.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);

                // Sao chép dữ liệu từ DataStream vào Bitmap
                for (int y = 0; y < description.Height; y++)
                {
                    Utilities.CopyMemory(bitmapData.Scan0 + y * bitmapData.Stride, dataStream + y * dataRowPitch, description.Width * 4);
                }

                // Mở khóa các bit của bitmap
                bitmap.UnlockBits(bitmapData);

                // Lưu bitmap vào tệp
                bitmap.Save(filePath, ImageFormat.Png);

                // Giải phóng tài nguyên
                bitmap.Dispose();
                device.ImmediateContext.UnmapSubresource(stagingTexture, 0);
            }
        }
    }
}