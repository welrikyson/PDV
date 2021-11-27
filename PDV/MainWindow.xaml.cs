using System.Windows;


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

        private void MainGrid_MKeyDown(object sender, RoutedEventArgs e)
        {
            ViewModels.MenuOptions menuOptions = new ();

            GereralMenu viewMenuOptions = new()
            {
                DataContext = menuOptions
            };

            Dialog.DialogService.ShowDialog(viewMenuOptions);

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