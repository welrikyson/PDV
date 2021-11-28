using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace PDV.ViewModels
{
    public class MenuOptions
    {
        public string Title { get; set; } = "Menu geral";
        public MenuOptionItem MenuOptionSelected { get; set; }
        public ICommand ConfirmSelection { get; set; }
        public MenuOption MenuOption { get; set; }

        public MenuOptions()
        {
            ConfirmSelection = new RelayCommand(ConfirmSelectionExecute);

            MenuOption = new MenuOption(optionFindItem: new MenuOptionItem(Key.D2, "FindItem", OnOptionSectedHandler),
                                        optionClose: new MenuOptionItem(Key.D3, "FindItem", OnOptionSectedHandler),
                                        optionEnd: new MenuOptionItem(Key.D4, "FindItem", OnOptionSectedHandler));


            MenuOptionSelected = MenuOption.OptionFindItem;

        }


        private void OnOptionSectedHandler(MenuOptionItem obj)
        {
            MenuOptionSelected = obj;
            ConfirmSelection.Execute(null);
        }

        internal void RefreshValues()
        {
            MenuOptionSelected = MenuOption.OptionFindItem;
        }

        private void ConfirmSelectionExecute()
        {
            OptionSelected?.Invoke();
        }

        public event OnConfirmSelection? OptionSelected;

    }
    public delegate void OnConfirmSelection();
    public class MenuOptionItem
    {
        public Key Key { get; set; }

        public string Description { get; set; }

        public ICommand Selected { get; set; }

        public MenuOptionItem(Key key, string description, Action<MenuOptionItem> onOptionSectedHandler)
        {
            Key = key;
            Description = description;
            Selected = new RelayCommand(OnSelected);
            OnOptionSelected += onOptionSectedHandler;
        }

        private void OnSelected()
        {
            OnOptionSelected?.Invoke(this);
        }

        public event Action<MenuOptionItem>? OnOptionSelected;

    }

    public class MenuOption
    {
        public MenuOptionItem OptionFindItem { get; set; }

        public MenuOptionItem OptionClose { get; set; }

        public MenuOptionItem OptionEnd { get; set; }

        public MenuOption(MenuOptionItem optionFindItem, MenuOptionItem optionClose, MenuOptionItem optionEnd)
        {
            OptionFindItem = optionFindItem;
            OptionClose = optionClose;
            OptionEnd = optionEnd;
        }
    }
}
