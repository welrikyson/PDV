using PDV.Commands;
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
        }
        private void Dialog_Closed(object sender, RoutedEventArgs e)
        {
            MainGrid.SetFocus();
        }
    }
}