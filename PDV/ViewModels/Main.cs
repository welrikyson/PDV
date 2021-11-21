using PDV.Commands;
using PDV.Interfaces;
using PDV.Mvvm;
using System.Windows.Input;

namespace PDV.ViewModels
{
    public class Main : NotifyPropertyChanged
    {

        public GeneralMenu GeneralMenu { get; set; } = new GeneralMenu();
        public Main()
        {
            Current = new ProductListManager(new Cancel(this), new ProductListFinalize(this));
            OpenMenuGeral = new OpenMenuGeral(this);
            CloseMenuGeneral = new CloseMenu(this);
        }

        public INavegable Current { get; private set; }

        public void NavigateToSalesInfo(ProductListManager from)
        {
            Current = new SaleInfo(from, new Cancel(this), new BackToAddMoreItems(this, from));

            NotifyChanged(nameof(Current));
        }

        public void NavigateToProductListManager()
        {
            Current = new ProductListManager(
                new Cancel(this),
                new ProductListFinalize(this));
            Current.ShouldExecutePreset = true;
            NotifyChanged(nameof(Current));
        }

        public void NavigateToProductListManager(INavegable lastProductManager)
        {
            Current = lastProductManager;
            Current.ShouldExecutePreset = true;
            NotifyChanged(nameof(Current));
        }

        public ICommand OpenMenuGeral { get; }
        public ICommand CloseMenuGeneral { get; }

        private bool _isMenuGeralOpen;
        public bool IsMenuGeralOpen
        {
            get => _isMenuGeralOpen;
            set { _isMenuGeralOpen = value; NotifyChanged(); }
        }
    }
}