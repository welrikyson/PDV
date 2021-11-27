using PDV.Interfaces;

namespace PDV.ViewModels
{
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