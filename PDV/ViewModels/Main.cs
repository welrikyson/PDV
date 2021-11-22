using PDV.Commands;
using PDV.Interfaces;
using PDV.Mvvm;
using System.Windows.Input;

namespace PDV.ViewModels
{
    public class Main : NotifyPropertyChanged
    {

        public GeneralMenu GeneralMenu { get; set; } = new GeneralMenu();
        public Navigator Navigator { get; set; }
        public Main(Navigator navigator)
        {            
            Navigator = navigator;
            OpenMenuGeral = new OpenMenuGeral(this);
            CloseMenuGeneral = new CloseMenu(this);
            Navigator.NavigateToProductListManager();
        }

        public INavegable Current { get=> Navigator.Current; }

        public ICommand OpenMenuGeral { get; }
        public ICommand CloseMenuGeneral { get; }

        private bool _isMenuGeralOpen = true;
        public bool IsMenuGeralOpen
        {
            get => _isMenuGeralOpen;
            set { _isMenuGeralOpen = value; NotifyChanged(); }
        }
    }
}