using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PDV.Views
{
    /// <summary>
    /// Interaction logic for Sale.xaml
    /// </summary>
    public partial class Sale
    {
        private ViewModels.Sale? viewmodel;

        public Sale()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (DataContext is ViewModels.Sale sale)
            {
                viewmodel = sale;
            }

            saleGrid.KeyDown += SaleGridKeyPress;
        }

        private void SaleGridKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.M)
            {
                ShowGeneralMenuDialog();
                e.Handled = true;
            }
        }

        private void ShowGeneralMenuDialog()
        {
            var menu = new GereralMenu()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };            

            ViewModels.MenuOptions menuoptions = new();
            
            menuoptions.EventResult += (opt) =>
            {
                ContentGrid.Children.Remove(menu);
                if (opt != null)
                {
                    OptionSelected(opt);
                }
            };

            menu.DataContext = menuoptions;
            OpenDialog(menu);            
        }
        private void ConfigureEsc(UIElement element)
        {
            element.KeyDown += (s, e) =>
             {
                 if (e.Key == Key.Escape)
                 {
                     CloseDialogAndRestoreFocus(element);
                 }
             };
        }
        private void OptionSelected(ViewModels.MenuOptionItem opt)
        {
            if (opt is ViewModels.MenuOptionFind)
            {
                ViewModels.ProductFinder productFinder = new();
                var finder = new ProductFinder()
                {
                    DataContext = productFinder,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };

                productFinder.EventResult += (produtct) =>
                {
                    ContentGrid.Children.Remove(finder);
                    saleGrid.IsEnabled = true;
                    UpdateLayout();
                    RestoreFocus();
                    if (produtct != null)
                    {
                        viewmodel?.Cart.AddItem(new Models.CartItem(produtct, 1));
                    }
                };
                OpenDialog(finder);
                finder.search.Focus();
            }
        }

        void OpenDialog(UIElement element)
        {
            ContentGrid.Children.Add(element);
            saleGrid.IsEnabled = false;
            ConfigureEsc(element);
            UpdateLayout();
            element.Focus();
        }

        void CloseDialogAndRestoreFocus(UIElement element)
        {
            ContentGrid.Children.Remove(element);
            saleGrid.IsEnabled = true;
            var uielement = FocusManager.GetFocusedElement(saleGrid);
            uielement.Focus();
        }

        void RestoreFocus()
        {
            var uielement = FocusManager.GetFocusedElement(saleGrid);
            uielement.Focus();
        }
    }
}
