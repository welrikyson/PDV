using System.Windows.Input;
using PDV.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace PDV.ViewModels
{
    public class SaleInfo : ObservableObject, INavegable
    {
        public SaleInfo(ProductListManager listManager)
        {
            ListManager = listManager;            
        }

        public ProductListManager ListManager { get; }
        
        public bool ShouldExecutePreset { get; set; }
    }
}