using System.Windows.Input;
using PDV.Interfaces;
using PDV.Mvvm;

namespace PDV.ViewModels
{
    public class SaleInfo : NotifyPropertyChanged, INavegable
    {
        public SaleInfo(ProductListManager listManager, ICommand cancel, ICommand backToAddMoreItems)
        {
            ListManager = listManager;
            Cancel = cancel;
            BackToAddMoreItems = backToAddMoreItems;
        }

        public ProductListManager ListManager { get; }
        public ICommand Cancel { get; }

        public ICommand BackToAddMoreItems { get; set; }
        public bool ShouldExecutePreset { get; set; }
    }
}