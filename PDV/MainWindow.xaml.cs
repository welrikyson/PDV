using PDV.Ultis.Moc;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

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
            frame.Navigate(new ProductListChart()
            {
                DataContext = new ViewModels.ProductListCart(Mock.CartItems)
            });
        }        

        private void MainGrid_MKeyDown(object sender, RoutedEventArgs e)
        {
            ViewModels.MenuOptions menuOptions = new();

            GereralMenu viewMenuOptions = new()
            {
                DataContext = menuOptions
            };

            Dialog.DialogService.ShowDialog(viewMenuOptions);
            Dialog.Closed += (s,e) =>
            {
                if (frame.Content is UIElement page)
                {
                    page.Focus();
                }
            };
            menuOptions.OptionSelected += () =>
            {
                Dialog.DialogService.CloseDialog();
                if (frame.Content is UIElement page)
                {
                    page.Focus();
                }
            };
        }
    }    
}