using MvvmDialogs.DialogTypeLocators;
using System;
using System.ComponentModel;

namespace PDV
{
    internal class DialogTypeLocator : IDialogTypeLocator
    {
        public Type Locate(INotifyPropertyChanged viewModel)
        {
            var viewModelType = viewModel.GetType();

            var dialogFullName = viewModelType.FullName;
            dialogFullName = dialogFullName.Substring(
                0,
                dialogFullName.Length - "Model".Length);

            return viewModelType.Assembly.GetType(dialogFullName);

        }

    }
}