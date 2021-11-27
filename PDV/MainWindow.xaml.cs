

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
            frame.Navigate(new DefaultPage());
        }

        private void Dialog_Closed(object sender, System.Windows.RoutedEventArgs e)
        {
            MainGrid.SetFocus(); 
        }

        private void MainGrid_MKeyDown(object sender, System.Windows.RoutedEventArgs e)
        {
            var menuOptions = new ViewModels.MenuOptions();
            
            Dialog.DialogService.ShowDialog(new Views.GereralMenu()
            {
                DataContext = menuOptions
            });
            menuOptions.OptionSelected += () =>
            {
                Dialog.DialogService.CloseDialog();
            };
        }
    }
}