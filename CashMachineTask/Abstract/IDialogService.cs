using System;

namespace CashMachineTask.Abstract
{
    interface IDialogService
    {
        void ShowDialog<TVIewModel>(IDialogParametrs dialogParametrs, Action<bool?, object> callback);

        void ShowDialog(IDialogViewModel dialogViewModel, Action<bool?, object> callback);
    }
}