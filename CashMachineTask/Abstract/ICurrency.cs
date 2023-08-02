namespace CashMachineTask.Abstract
{
    internal interface ICurrency
    {
        string Title { get; }

        decimal Denomination { get; }
    }
}