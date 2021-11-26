using PDV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
