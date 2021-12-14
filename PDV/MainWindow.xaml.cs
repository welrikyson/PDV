using PDV.Ultis.Moc;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Web;

namespace PDV.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        MainViewModel? ViewModel;
        public MainWindow()
        {
            InitializeComponent();            
            if(DataContext is MainViewModel viewModel)
            {
                ViewModel = viewModel;
            }
        }
    }
}