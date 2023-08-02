namespace CashMachineTask.Abstract
{
    internal interface ICassette<T> where T : ICurrency
    {
        T StoredCurrency { get; }

        int Quantity { get; }

        int Capacity { get; }

        bool AddBanknots(int currencyCount);
    }
}