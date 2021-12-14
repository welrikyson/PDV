using Microsoft.Toolkit.Mvvm.ComponentModel;
using PDV.Interfaces;
using System.Threading.Tasks;

namespace PDV.ViewModels
{
    public class DialogService : IDialogService
    {
        public object? Current { get; private set; }
        public event CloseDialogEvent? CloseDialogEvent;
        public event OpenDialogEvent? OpenDialogEvent;
        public void CloseDialog()
        {
            if (Current != null)
                CloseDialogEvent?.Invoke(Current);
            if(closeDialogOnTask != null)
            {
                closeDialogOnTask?.Invoke();
                closeDialogOnTask= null;
            }
            
        }
        private delegate void OnCloseDialogInTask();

        private OnCloseDialogInTask? closeDialogOnTask;
        public Task<T?> Show<T>(IDialog<T> dialog)
        {
            TaskCompletionSource<T?> taskCompletion = new();
            dialog.EventResult += (t) =>
            {
                taskCompletion.SetResult(t);
                CloseDialogEvent?.Invoke(dialog);
            };
            closeDialogOnTask = new(() =>
            {
                if (!taskCompletion.Task.IsCompleted)
                {
                    taskCompletion.SetResult(default);
                }
            });
            ShowDialog(dialog);

            return taskCompletion.Task;
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