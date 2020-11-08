using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SheeterCore;

namespace Sheeter
{
    /// <summary>
    /// KeymapOverlayWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class KeymapOverlayWindow : Window
    {
        public int modifier = 0x0008; //Win key
        public int keys = (int)Keys.Scroll;
        public int id;
        public KeymapOverlayWindow()
        {
            InitializeComponent();
            return;
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x0312)
            {
                HookingEvent();
            }

            return IntPtr.Zero;
        }

        private void HookingEvent()
        {
            if (this.Visibility == Visibility.Hidden)
            {
                this.ActivateAndRead();
            }
            else
            {
                this.Hide();
            }
        }

        private void ActivateAndRead()
        {
            var (name, title) = Hook.GetActiveProcess();
            Search.Text = title;
            var info = Json.loadKeyMapFile(@$"./keymap/{title}.json");
            keymapGrid.ItemsSource = info.KeyMaps;
            this.Show();
            this.Activate();
        }

        private void Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;
            modifier = 0x0008; //Win key
            keys = (int)Keys.Scroll;
            id = handle.ToInt32() ^ modifier ^ keys;
            var a = Hook.RegisterHotKey(handle, id, modifier,keys);
            HwndSource source = HwndSource.FromHwnd(handle);
            source.AddHook(new HwndSourceHook(WndProc));
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;
            Hook.UnregisterHotKey(handle, id);
        }
    }
}
