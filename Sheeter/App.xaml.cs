using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static SheeterCore.Hook;

namespace Sheeter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon trayIcon;

        private static OverlayWindow overlayWindow = new OverlayWindow();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SetHook();
            CreateTray();
        }

        private void CreateTray()
        {
            CreateTrayIcon();
            CreateTrayMenu();
        }

        private void CreateTrayIcon()
        {
            trayIcon = new System.Windows.Forms.NotifyIcon();
            trayIcon.Icon = new System.Drawing.Icon("sheeter.ico");
            trayIcon.Visible = true;
        }

        private void CreateTrayMenu()
        {
            trayIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            trayIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }

        private void ExitApplication()
        {
            UnHook();
            MainWindow?.Close();
            trayIcon?.Dispose();
            trayIcon = null;
            System.Windows.Application.Current.Shutdown();
        }


        private LowLevelKeyboardProc _proc = hookProc;

        private static IntPtr windowsHook = IntPtr.Zero;

        public void SetHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            windowsHook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hInstance, 0);
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(windowsHook);
        }

        public static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == VK_CODE)
                    Overlay();
            }

            return CallNextHookEx(windowsHook, code, (int)wParam, lParam);
        }

        private static void Overlay()
        {
            if (overlayWindow == null)
                overlayWindow = new OverlayWindow();

            if (overlayWindow.IsVisible)
            {
                overlayWindow.Hide();
            }
            else
            {
                overlayWindow.SettingKeyMaps();
                overlayWindow.Show();
                overlayWindow.Activate();
                overlayWindow.Topmost = true;  // important
                overlayWindow.Topmost = false; // important
                overlayWindow.Focus();         // important
            }
        }
    }
}
