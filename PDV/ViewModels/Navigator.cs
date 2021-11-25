using PDV.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace PDV.ViewModels
{
    public class Navigator : ObservableObject
    {
        public Navigator(NavegableFactory navegableFactory)
        {
            NavegableFactory = navegableFactory;
        }
        public INavegable Current { get; private set; }
        public NavegableFactory NavegableFactory { get; }

        public void NavigateToSalesInfo(ProductListManager from)
        {
            Current = NavegableFactory.SaleInfo;
            OnPropertyChanged(nameof(Current));            
        }

        public void NavigateToProductListManager()
        {
            Current = NavegableFactory.ProductListManager;
            Current.ShouldExecutePreset = true;
            OnPropertyChanged(nameof(Current));
        }

        public void NavigateToProductListManager(INavegable lastProductManager)
        {
            Current = lastProductManager;
            Current.ShouldExecutePreset = true;
            OnPropertyChanged(nameof(Current));
        }
    }
}
