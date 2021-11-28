using System.Windows.Input;

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
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.Control)
            {
                NavigationToSalesInfo();
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationToSalesInfo();
        }

        private void NavigationToSalesInfo()
        {
            var salesInfo = new SaleInfo()
            {
                DataContext = new ViewModels.SaleInfo()
            };
            NavigationService.Navigate(salesInfo);
        }

        private void NumericUpDown_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                SearchTermTxb.Focus();
        }
    }
}