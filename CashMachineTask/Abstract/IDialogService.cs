using System;

namespace CashMachineTask.Abstract
{
    interface IDialogService
    {
        void ShowDialog<TResult>(IDialogViewModel dialogViewModel, Action<bool?, TResult> callback);
    }
}