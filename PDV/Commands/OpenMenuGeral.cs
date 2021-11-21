using PDV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PDV.Commands
{
    public class OpenMenuGeral : ICommand
    {
        public Main Main { get; }

        public event EventHandler? CanExecuteChanged;

        public OpenMenuGeral(Main main)
        {
            Main = main;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Main.IsMenuGeralOpen = true;
        }
    }
}
