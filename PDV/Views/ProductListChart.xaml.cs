namespace PDV.Views
{
    /// <summary>
    ///     Interaction logic for ProductListChart.xaml
    /// </summary>
    public partial class ProductListChart
    {
        public ProductListChart()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var salesInfo = new SaleInfo()
            {
                DataContext = new ViewModels.SaleInfo()
            }; 
            NavigationService.Navigate(salesInfo);
        }
    }
}