using PDV.ViewModels;

namespace PDV.Interfaces
{
    public interface IDialogService
    {
        void ShowDialog(object content);
        void CloseDialog();
        event CloseDialogEvent? CloseDialogEvent;
        event OpenDialogEvent? OpenDialogEvent;
    }
}
