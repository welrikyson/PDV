using PDV.Ultis.Moc;
using System.Windows;
using System.Windows.Navigation;

namespace PDV.Views
{
    /// <summary>
    /// Interaction logic for DefaultPage.xaml
    /// </summary>
    public partial class DefaultPage
    {
        public DefaultPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductListChart()
            {
                DataContext = new ViewModels.ProductListCart(Mock.CartItems)
            });
        }
    }
}
