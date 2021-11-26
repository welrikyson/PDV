using PDV.Commands;
using PDV.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using System.Threading.Tasks;
using System;
using Microsoft.Toolkit.Mvvm.Input;

namespace PDV.ViewModels
{
    public class Main : ObservableObject
    {
        private readonly Navigator _navigator;
        public IDialogService DialogService { get; }

        public INavegable Current { get => _navigator.Current; }
        public ICommand OpenMenuGeral { get; }

        public Main(Navigator navigator, IDialogService dialogService)
        {
            _navigator = navigator;
            this.DialogService = dialogService;
            OpenMenuGeral = new RelayCommand(ShowDialogMenu);
            _navigator.NavigateToProductListManager();
        }

        private void ShowDialogMenu()
        {
            MenuOptions menuOptions = new ();
            DialogService.ShowDialog(menuOptions);

            menuOptions.OptionSelected += () =>
            {
                DialogService.CloseDialog();
            };
        }
    }

    public class DialogService : IDialogService
    {
        public object? Current { get; set; }
        public event CloseDialogEvent? CloseDialogEvent;
        public event OpenDialogEvent? OpenDialogEvent;
        public void CloseDialog()
        {
            if (Current != null)
                CloseDialogEvent?.Invoke(Current);
        }

        public void ShowDialog(object content)
        {
            Current = content;
            OpenDialogEvent?.Invoke(content);
        }
    }

    public delegate void CloseDialogEvent(object content);

    public delegate void OpenDialogEvent(object content);
}