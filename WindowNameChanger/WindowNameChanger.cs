using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowNameChanger
{
    public class WindowNameChanger
    {

        public delegate bool CallBackPtr(int hwnd, int lParam);
        private static CallBackPtr callBackPtr;


        [DllImport("user32.dll")]
        private static extern int EnumWindows(CallBackPtr callPtr, int lPar);

        [DllImport("user32.dll")]
        static extern int GetWindowTextLength(IntPtr hWnd);


        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("psapi.dll")]
        private static extern uint GetModuleFileNameEx(IntPtr hWnd, IntPtr hModule, StringBuilder lpFileName, int nSize);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);

        static Dictionary<IntPtr, string> Windows = new Dictionary<IntPtr, string>();

        public static bool Report(int hWnd, int lParam)
        {
            //Console.WriteLine("Window handle is " + hwnd);

            int length = GetWindowTextLength((IntPtr)hWnd);
            StringBuilder text = new StringBuilder(length + 1);
            GetWindowText((IntPtr)hWnd, text, text.Capacity);

            string name = text.ToString();

            Windows.Add((IntPtr)hWnd, name);

            return true;
        }

        private static void RefreshWindowsList()
        {
            Windows.Clear();
            callBackPtr = new CallBackPtr(WindowNameChanger.Report);
            WindowNameChanger.EnumWindows(callBackPtr, 0);
        }

        public static WindowListItem getWindow()
        {
            RefreshWindowsList();

            KeyValuePair<IntPtr, string> window = Windows.Where(kp => kp.Value.Equals("The GioiHoan My", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            return new WindowListItem { HWnd = window.Key, WindowName = window.Value };
        }

    }

    public class WindowListItem
    {
        public IntPtr HWnd;
        public string WindowName = string.Empty;

        public override string ToString()
        {
            return WindowName;
        }
    }
}
