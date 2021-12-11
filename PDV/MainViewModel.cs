using Microsoft.Toolkit.Mvvm.Input;
using PDV.Interfaces;
using PDV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PDV
{
    public class MainViewModel
    {
        public IDialogService? DialogService { get; set; }
        public MainViewModel()
        {
            OpenMenu = new RelayCommand(OpenMenuHandler);
            DialogService = new DialogService();
        }

        private void OpenMenuHandler()
        {
            var menuOptions = new MenuOptions();
            DialogService?.ShowDialog(menuOptions);
            menuOptions.OptionSelected += (s) =>
            {
                DialogService?.CloseDialog();
            };
        }

        public ICommand OpenMenu { get; set; }
    }
}
