using System;
using System.Windows.Input;
using PDV.ViewModels;

namespace PDV.Commands
{
    public class ProductListFinalize : ICommand
    {
        private readonly Main _main;

        public ProductListFinalize(Main main)
        {
            _main = main;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (_main.Current is ProductListManager productListManager)
                _main.NavigateToSalesInfo(productListManager);
        }
    }
}