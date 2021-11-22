using System;
using System.Windows.Input;
using PDV.ViewModels;

namespace PDV.Commands
{
    public class BackToAddMoreItems : ICommand
    {
        public BackToAddMoreItems(Navigator navigator, ProductListManager lastProductManager)
        {
            Navigator = navigator;
            LastProductManager = lastProductManager;
        }

        public Navigator Navigator { get; }

        private ProductListManager LastProductManager { get; }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Navigator.NavigateToProductListManager(LastProductManager);
        }
    }
}