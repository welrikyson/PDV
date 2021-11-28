using Microsoft.Toolkit.Mvvm.ComponentModel;
using PDV.Interfaces;

namespace PDV.ViewModels
{
    public class SaleInfo : ObservableObject, INavegable
    {
        public bool ShouldExecutePreset { get; set; }
    }
}