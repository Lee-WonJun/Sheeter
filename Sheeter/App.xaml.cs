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

        //private static OverlayWindow overlayWindow = new OverlayWindow();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
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
            MainWindow?.Close();
            trayIcon?.Dispose();
            trayIcon = null;
            Current.Shutdown();
        }
    }
}
