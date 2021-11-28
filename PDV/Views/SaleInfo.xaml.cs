namespace PDV.Views
{
    /// <summary>
    ///     Interaction logic for SaleInfo.xaml
    /// </summary>
    public partial class SaleInfo
    {
        public SaleInfo()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new DefaultPage());
        }
    }
}