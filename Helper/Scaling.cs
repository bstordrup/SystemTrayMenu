﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace SystemTrayMenu.Helper
{
    internal static class Scaling
    {
        internal static float Factor = 1;

        internal static void Initialize()
        {
            CalculateScalingFactor();
            SetProcessDPIAwareWhenNecessary();
        }

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
            // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        }
        static void CalculateScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            Factor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight; // 1.25 = 125%

        }

        [DllImport("user32.dll")]
        static extern bool SetProcessDPIAware();
        static void SetProcessDPIAwareWhenNecessary()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }
        }

    }
}
