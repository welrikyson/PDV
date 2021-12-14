using PDV.ViewModels;
using System.Threading.Tasks;

namespace PDV.Interfaces
{
    public interface IDialogService
    {
        void ShowDialog(object content);        
        void CloseDialog();
        Task<T?> Show<T>(IDialog<T> dialog);

        event CloseDialogEvent? CloseDialogEvent;
        event OpenDialogEvent? OpenDialogEvent;
    }

    public interface IDialog<TResult>
    {
        event EventResult<TResult> EventResult;
    }

    public delegate void EventResult<T>(T? result);
}
