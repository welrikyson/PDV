using PDV.Commands;
using PDV.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using MvvmDialogs;
using System.Threading.Tasks;

namespace PDV.ViewModels
{
    public class Main : ObservableObject
    {

        public GeneralMenu GeneralMenu { get; set; } = new GeneralMenu();
        private readonly Navigator _navigator;
        private readonly IDialogService dialogService;

        public Main(Navigator navigator, IDialogService dialogService)
        {            
            _navigator = navigator;
            this.dialogService = dialogService;
            OpenMenuGeral = new OpenMenuGeral(this);
            CloseMenuGeneral = new CloseMenu(this);
            _navigator.NavigateToProductListManager();
            IsMenuGeralOpen = false;         
        }
        
        public INavegable Current { get=> _navigator.Current; }

        public ICommand OpenMenuGeral { get; }
        public ICommand CloseMenuGeneral { get; }

        private bool _isMenuGeralOpen = true;
        public bool IsMenuGeralOpen
        {
            get => _isMenuGeralOpen;
            set { SetProperty(ref _isMenuGeralOpen, value); }
        }
    }
}