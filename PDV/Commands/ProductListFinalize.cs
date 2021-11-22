using System;
using System.Windows.Input;
using PDV.ViewModels;

namespace PDV.Commands
{
    public class ProductListFinalize : ICommand
    {
        public ProductListFinalize(Navigator navigator)
        {
            Navigator = navigator;
        }

        public Navigator Navigator { get; }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (Navigator.Current is ProductListManager productListManager)
                Navigator.NavigateToSalesInfo(productListManager);
        }
    }
}