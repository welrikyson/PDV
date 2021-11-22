using PDV.Interfaces;
using PDV.Mvvm;

namespace PDV.ViewModels
{
    public class Navigator : NotifyPropertyChanged
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

            NotifyChanged(nameof(Current));
        }

        public void NavigateToProductListManager()
        {
            Current = NavegableFactory.ProductListManager;
            Current.ShouldExecutePreset = true;
            NotifyChanged(nameof(Current));
        }

        public void NavigateToProductListManager(INavegable lastProductManager)
        {
            Current = lastProductManager;
            Current.ShouldExecutePreset = true;
            NotifyChanged(nameof(Current));
        }
    }
}
