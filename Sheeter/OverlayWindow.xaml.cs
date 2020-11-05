using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SheeterCore;

namespace Sheeter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Window
    {
        public OverlayWindow()
        {
            InitializeComponent();
            observableExample = new ObservableCollection<string>();
            KeyMapList.ItemsSource = observableExample;
        }

        public ObservableCollection<string> observableExample { get; set; }

        internal void SettingKeyMaps()
        {
            observableExample.Clear();
            var (title, name) = Hook.GetActiveProcess();

            var keys = Json.loadKeyMapFile($@".\keymaps\{name}.json");
            
            keys.ToList().ForEach(x=> observableExample.Add($"{x.Keys}:{x.Action}"));
        }
    }
}
