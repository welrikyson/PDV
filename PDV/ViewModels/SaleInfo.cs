using System.Windows.Input;
using PDV.Interfaces;
using PDV.Mvvm;

namespace PDV.ViewModels
{
    public class SaleInfo : NotifyPropertyChanged, INavegable
    {
        public SaleInfo(ProductListManager listManager)
        {
            ListManager = listManager;            
        }

        public ProductListManager ListManager { get; }
        
        public bool ShouldExecutePreset { get; set; }
    }
}