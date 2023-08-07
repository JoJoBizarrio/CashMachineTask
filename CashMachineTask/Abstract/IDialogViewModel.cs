namespace CashMachineTask.Abstract
{
    internal interface IDialogViewModel
    {
        void OnDialogOpened(IDialogParametrs parametrs);

        object Result { get; }
    }
}
