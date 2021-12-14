using Microsoft.Toolkit.Mvvm.ComponentModel;
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
    public class MainViewModel : ObservableObject
    {
        public IDialogService DialogService { get; }
        public ObservableObject CurrentViewModel { get; private set; }
        public MainViewModel()
        {
            OpenMenu = new AsyncRelayCommand(OpenMenuHandlerAsync);
            DialogService = new DialogService();
            var sale = new Sale();
            sale.FinalizeEventHandler += () =>
            {
                CurrentViewModel = new SaleInfo();
                this.OnPropertyChanged(nameof(CurrentViewModel));

            };
            CurrentViewModel = sale;
        }

        private async Task OpenMenuHandlerAsync()
        {
            var menuOptions = new MenuOptions();
            var option = await DialogService.Show(menuOptions);
            if (option == null) return;

            if (option is MenuOptionFind)
            {
                var product = await DialogService.Show(new ProductFinder());

                if (product != null && CurrentViewModel is Sale sale)
                {
                    sale.Cart.AddItem(new Models.CartItem(product, 1));
                }
            }

        }

        public ICommand OpenMenu { get; set; }
    }
}
