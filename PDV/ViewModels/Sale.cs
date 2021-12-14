using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PDV.Ultis.Moc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PDV.ViewModels
{
    public class Sale : ObservableObject
    {
        public Sale()
        {            
            Cart = new Cart(Mock.CartItems);
            Finalize = new RelayCommand(FinalizeHandler);
        }
        public event Action? FinalizeEventHandler;
        private void FinalizeHandler()
        {
            FinalizeEventHandler?.Invoke();
            //navigate Finalize Sale screen
        }

        public ICommand Finalize { get; set; }

        public Cart Cart { get; }
    }
}
