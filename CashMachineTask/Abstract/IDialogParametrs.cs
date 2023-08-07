namespace CashMachineTask.Abstract
{
    public interface IDialogParametrs
    {
        T GetValue<T>(string parametrName);
        object GetValue(string parametrName);
    }
}