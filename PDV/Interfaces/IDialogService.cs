using PDV.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace PDV.Interfaces
{
    public interface IDialogService
    {
        void ShowDialog(object content);        
        void CloseDialog();
        Task<T?> Show<T>(IDialog<T> dialog);

        event CloseDialogEvent? CloseDialogEvent;
        event OpenDialogEvent? OpenDialogEvent;

        void DisposeTask(object sender, RoutedEventArgs e);
    }

    public interface IDialog<TResult>
    {
        event EventResult<TResult> EventResult;        
    }

    public delegate void EventResult<T>(T? result);
}
