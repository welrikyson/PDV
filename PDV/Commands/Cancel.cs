using System;
using System.Windows.Input;
using PDV.ViewModels;

namespace PDV.Commands
{
    public class Cancel : ICommand
    {
        public Cancel(Main main)
        {
            Main = main;
        }

        private Main Main { get; }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Main.NavigateToProductListManager();
            OnCancel?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? OnCancel;
    }
}