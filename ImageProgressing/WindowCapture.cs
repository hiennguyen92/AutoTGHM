using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using System;
using System.Windows.Forms;

using Windows.Graphics.Capture;
using Windows.Graphics.DirectX;
using Windows.Graphics.DirectX.Direct3D11;



using ImageProgressing.Interfaces;
using ImageProgressing.Interop;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.InteropServices;
using SharpDX.Windows;
using Emgu.CV.Ocl;
using System.IO;

namespace ImageProgressing
{
    public class WindowCapture: IDisposable, ICaptureMethod
    {

        private static readonly Guid _graphicsCaptureItemIid = new Guid("79C3F95B-31F7-4EC2-A464-632EF5D30760");
        private Direct3D11CaptureFramePool _captureFramePool;
        private GraphicsCaptureItem _captureItem;
        private GraphicsCaptureSession _captureSession;

        private Form _fmParent;
        private IntPtr _captureHandle;
        private String _file;


        public WindowCapture(Form form, IntPtr hWnd, string file)
        {
            this._fmParent = form;
            this._captureHandle = hWnd;
            _file = file;
        }

        public void Start()
        {
            // create a Device and SwapChain
            var swapChainDescription = new SwapChainDescription
            {
                BufferCount = 2,
                Flags = SwapChainFlags.None,
                IsWindowed = true,
                ModeDescription = new ModeDescription(this._fmParent.ClientSize.Width, this._fmParent.ClientSize.Height, new Rational(60, 1), Format.B8G8R8A8_UNorm),
                OutputHandle = this._fmParent.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, swapChainDescription, out var device, out var swapChain);
            using var swapChain1 = swapChain.QueryInterface<SwapChain1>();

            // ignore all Windows events
            using var factory = swapChain1.GetParent<Factory>();
            factory.MakeWindowAssociation(this._fmParent.Handle, WindowAssociationFlags.IgnoreAll);


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
            using var backBuffer = Resource.FromSwapChain<Texture2D>(swapChain1, 0);
            using var renderTargetView = new RenderTargetView(device, backBuffer);

            device.ImmediateContext.Rasterizer.SetViewport(0, 0, 1, 1);
            device.ImmediateContext.OutputMerger.SetTargets(renderTargetView);

            while (true)
            {
                if (!IsCapturing)
                    StartCapture(_captureHandle, device, factory);

                // clear view
                device.ImmediateContext.ClearRenderTargetView(renderTargetView, new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));

                using var texture2d = TryGetNextFrameAsTexture2D(device);
                if (texture2d != null)
                {
                    SaveTexture2DToImage(device, texture2d, String.Format(@"{0}/screenshot.png", _file));
                    break;
                }
            }


            renderTargetView.Dispose();
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

                // Tạo thư mục nếu chưa tồn tại
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Lưu bitmap vào tệp
                bitmap.Save(filePath, ImageFormat.Png);

                // Giải phóng tài nguyên
                bitmap.Dispose();
                device.ImmediateContext.UnmapSubresource(stagingTexture, 0);
            }
        }



        public void Dispose() {
            StopCapture();
        }

        public void StopCapture()
        {
            _captureSession?.Dispose();
            _captureFramePool?.Dispose();
            _captureSession = null;
            _captureFramePool = null;
            _captureItem = null;
            IsCapturing = false;
        }

        public bool IsCapturing { get; private set; }

        public void StartCapture(IntPtr hWnd, Device device, Factory factory)
        {
            _captureItem = CreateItemForWindow(hWnd);

            if (_captureItem == null)
                return;

            _captureItem.Closed += CaptureItemOnClosed;

            var hr = NativeMethods.CreateDirect3D11DeviceFromDXGIDevice(device.NativePointer, out var pUnknown);
            if (hr != 0)
            {
                StopCapture();
                return;
            }

            var winrtDevice = (IDirect3DDevice)Marshal.GetObjectForIUnknown(pUnknown);
            Marshal.Release(pUnknown);

            _captureFramePool = Direct3D11CaptureFramePool.Create(winrtDevice, DirectXPixelFormat.B8G8R8A8UIntNormalized, 2, _captureItem.Size);
            _captureSession = _captureFramePool.CreateCaptureSession(_captureItem);
            _captureSession.StartCapture();
            IsCapturing = true;
        }

        public Texture2D TryGetNextFrameAsTexture2D(Device device)
        {
            using var frame = _captureFramePool?.TryGetNextFrame();
            if (frame == null)
                return null;

            // ReSharper disable once SuspiciousTypeConversion.Global
            var surfaceDxgiInterfaceAccess = (IDirect3DDxgiInterfaceAccess)frame.Surface;
            var pResource = surfaceDxgiInterfaceAccess.GetInterface(new Guid("dc8e63f3-d12b-4952-b47b-5e45026a862d"));

            using var surfaceTexture = new Texture2D(pResource); // shared resource
            var texture2dDescription = new Texture2DDescription
            {
                ArraySize = 1,
                BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = Format.B8G8R8A8_UNorm,
                Height = surfaceTexture.Description.Height,
                MipLevels = 1,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                Width = surfaceTexture.Description.Width
            };
            var texture2d = new Texture2D(device, texture2dDescription);
            device.ImmediateContext.CopyResource(surfaceTexture, texture2d);

            return texture2d;
        }


        private static GraphicsCaptureItem CreateItemForWindow(IntPtr hWnd)
        {
            var factory = WindowsRuntimeMarshal.GetActivationFactory(typeof(GraphicsCaptureItem));
            var interop = (IGraphicsCaptureItemInterop)factory;
            var pointer = interop.CreateForWindow(hWnd, typeof(GraphicsCaptureItem).GetInterface("IGraphicsCaptureItem").GUID);
            var capture = Marshal.GetObjectForIUnknown(pointer) as GraphicsCaptureItem;
            Marshal.Release(pointer);

            return capture;
        }

        private void CaptureItemOnClosed(GraphicsCaptureItem sender, object args)
        {
            StopCapture();
        }
    }
}
