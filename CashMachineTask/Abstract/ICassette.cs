namespace CashMachineTask.Abstract
{
    internal interface ICassette
    {
        ICurrency StoredCurrency { get; }

        int Quantity { get; }

        int Capacity { get; }

        bool AddCurrency(int currencyCount);
    }
}