using System;
using System.Windows.Input;
using PDV.ViewModels;

namespace PDV.Commands
{
    public class BackToAddMoreItems : ICommand
    {
        public BackToAddMoreItems(Main main, ProductListManager lastProductManager)
        {
            Main = main;
            LastProductManager = lastProductManager;
        }

        private Main Main { get; }
        private ProductListManager LastProductManager { get; }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Main.NavigateToProductListManager(LastProductManager);
        }
    }
}