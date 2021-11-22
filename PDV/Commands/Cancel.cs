using System;
using System.Windows.Input;
using PDV.ViewModels;

namespace PDV.Commands
{
    public class Cancel : ICommand
    {
        public Cancel(Navigator navigator)
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
            Navigator.NavigateToProductListManager();
            OnCancel?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? OnCancel;
    }
}