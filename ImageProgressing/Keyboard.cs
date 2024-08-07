﻿using Emgu.CV.Ocl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageProgressing
{
    public class Keyboard
    {
        [DllImport("user32.dll")]
        public static extern int SendMessageA(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_WHEEL = 0x0800;
        private const int MOUSEEVENTF_XDOWN = 0x0080;
        private const int MOUSEEVENTF_XUP = 0x0100;
        ///summary>
        /// Virtual Messages
        /// </summary>
        public static bool d1;
        public static bool d2;
        public static bool d3;
        public static bool d4;
        public static bool d5;
        public static bool d6;
        public static bool d7;
        public static bool d8;
        public static bool d9;
        public static bool d0;
        public static bool f1;
        public static bool f2;
        public static bool f3;
        public static bool f4;
        public static bool f5;
        public static bool f6;
        public static bool f7;
        public static bool f8;
        public static bool f9;
        public static bool f10;

        public static int Sleepd1;
        public static int Sleepd2;
        public static int Sleepd3;
        public static int Sleepd4;
        public static int Sleepd5;
        public static int Sleepd6;
        public static int Sleepd7;
        public static int Sleepd8;
        public static int Sleepd9;
        public static int Sleepd0;
        public static int Sleepf1;
        public static int Sleepf2;
        public static int Sleepf3;
        public static int Sleepf4;
        public static int Sleepf5;
        public static int Sleepf6;
        public static int Sleepf7;
        public static int Sleepf8;
        public static int Sleepf9;
        public static int Sleepf10;

        public static int PauseBetweenSkills = 100;
        public static bool KeyPress = false;

        public static void MouseMove(uint x, uint y)
        {
            mouse_event(0x0001, x, y, 0, 0);
        }

        public enum WMessages : int
        {
            WM_LBUTTONDOWN = 0x201, //Left mousebutton down
            WM_LBUTTONUP = 0x202,  //Left mousebutton up
            WM_LBUTTONDBLCLK = 0x203, //Left mousebutton doubleclick
            WM_RBUTTONDOWN = 0x204, //Right mousebutton down
            WM_RBUTTONUP = 0x205,   //Right mousebutton up
            WM_RBUTTONDBLCLK = 0x206, //Right mousebutton doubleclick
            WM_KEYDOWN = 0x100,  //Key down
            WM_KEYUP = 0x101,   //Key up
            WM_CHAR = 0x0102,
            VK_RETURN = 0x0D,
        }
        public static Random r;
       

        public static void TabKey(IntPtr hWnd)
        {
            if (!KeyPress)
            {
                SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.Tab, (IntPtr)0);
                SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.Tab, (IntPtr)0);
                Thread.Sleep(10);
            }
        }
        public static void pressKey(IntPtr hWnd, string key)
        {
            var task = Task.Factory.StartNew(() =>
            {
                if (hWnd != IntPtr.Zero)
                {
                    KeyPress = true;

                    if (f1 && key == "F1")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.F1, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.F1, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepf1);
                        KeyPress = false;
                    }
                    if (f2 && key == "F2")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.F2, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.F2, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepf2);
                        KeyPress = false;
                    }
                    if (f3 && key == "F3")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.F3, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.F3, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepf3);
                        KeyPress = false;
                    }
                    if (f4 && key == "F4")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.F4, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.F4, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepf4);
                        KeyPress = false;
                    }
                    if (f5 && key == "F5")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.F5, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.F5, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepf5);
                        KeyPress = false;
                    }
                    if (f6 && key == "F6")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.F6, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.F6, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepf6);
                        KeyPress = false;
                    }
                    if (f7 && key == "F7")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.F7, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.F7, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepf7);
                        KeyPress = false;
                    }
                    if (f8 && key == "F8")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.F8, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.F8, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepf8);
                        KeyPress = false;
                    }
                    if (f9 && key == "F9")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.F9, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.F9, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepf9);
                        KeyPress = false;
                    }
                    if (f10 && key == "F10")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.F10, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.F10, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepf10);
                        KeyPress = false;
                    }
                    if (d1 && key == "D1")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.D1, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.D1, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepd1);
                        KeyPress = false;
                    }
                    if (d2 && key == "D2")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.D2, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.D2, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        KeyPress = false;
                    }
                    if (d3 && key == "D3")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.D3, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.D3, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepd3);
                        KeyPress = false;
                    }
                    if (d4 && key == "D4")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.D4, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.D4, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepd4);
                        KeyPress = false;
                    }
                    if (d5 && key == "D5")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.D5, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.D5, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepd5);
                        KeyPress = false;
                    }
                    if (d6 && key == "D6")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.D6, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.D6, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepd6);
                        KeyPress = false;
                    }
                    if (d7 && key == "D7")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.D7, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.D7, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepd7);
                        KeyPress = false;
                    }
                    if (d8 && key == "D8")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.D8, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.D8, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepd8);
                        KeyPress = false;
                    }
                    if (d9 && key == "D9")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.D9, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.D9, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepd9);
                        KeyPress = false;
                    }
                    if (d0 && key == "D0")
                    {
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYDOWN, (IntPtr)System.Windows.Forms.Keys.D0, (IntPtr)0);
                        Thread.Sleep(25);
                        SendMessageA(hWnd, (int)Keyboard.WMessages.WM_KEYUP, (IntPtr)System.Windows.Forms.Keys.D0, (IntPtr)0);
                        Thread.Sleep(PauseBetweenSkills);
                        Thread.Sleep(Sleepd0);
                        KeyPress = false;
                    }
                }
            });
        }

    }
}
