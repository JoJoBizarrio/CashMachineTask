using System.Windows;

namespace CashMachineTask.Abstract
{
    internal interface IDialogViewModel
    {
        void OnDialogOpened(IDialogParametrs parametrs);

        object Result { get; }

        Window Owner { get; }
    }
}
