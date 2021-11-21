using PDV.ViewModels;
using System;
using System.Windows.Input;

namespace PDV.Commands
{
    public class CloseMenu : ICommand
    {
        public Main Main { get; }

        public event EventHandler? CanExecuteChanged;

        public CloseMenu(Main main)
        {
            Main = main;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Main.IsMenuGeralOpen = false;
        }
    }
}
