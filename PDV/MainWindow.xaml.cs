using PDV.Ultis.Moc;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PDV.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {

            InitializeComponent();
            Dialog.Closed += (s, e) =>
            {
                frame.Focus();                
            };
            frame.Navigate(new ViewModels.Sale());
        }
    }
}