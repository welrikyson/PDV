using Microsoft.Toolkit.Mvvm.Input;
using PDV.Interfaces;
using System;
using System.Windows.Input;

namespace PDV.ViewModels
{
    public class MenuOptions: IDialog<MenuOptionItem>
    {
        public string Title { get; set; } = "Menu geral";
        public MenuOption MenuOption { get; set; }

        public MenuOptions()
        {
            MenuOption = new MenuOption(optionFindItem: new MenuOptionFind(OnOptionSectedHandler),
                                        optionClose: new MenuOptionItem(Key.D3, "Remove", OnOptionSectedHandler),
                                        optionEnd: new MenuOptionItem(Key.D4, "Other", OnOptionSectedHandler));

        }

        private void OnOptionSectedHandler(MenuOptionItem obj)
        {
            EventResult?.Invoke(obj);
            OptionSelected?.Invoke(obj);
        }

        public event OnConfirmSelection? OptionSelected;
        public event EventResult<MenuOptionItem>? EventResult;
    }
    public delegate void OnConfirmSelection(MenuOptionItem item);
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

    public class MenuOptionFind : MenuOptionItem
    {
        public MenuOptionFind(Action<MenuOptionItem> onOptionSectedHandler) : base(Key.D2, "Find Item", onOptionSectedHandler)
        {
        }
    }

    public class MenuOption
    {
        public MenuOptionFind OptionFindItem { get; set; }

        public MenuOptionItem OptionClose { get; set; }

        public MenuOptionItem OptionEnd { get; set; }

        public MenuOption(MenuOptionFind optionFindItem, MenuOptionItem optionClose, MenuOptionItem optionEnd)
        {
            OptionFindItem = optionFindItem;
            OptionClose = optionClose;
            OptionEnd = optionEnd;
        }
    }
}
