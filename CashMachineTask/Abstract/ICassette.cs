using System.Collections;

namespace CashMachineTask.Abstract
{
    internal interface ICassette
    {
        int Quantity { get; }
        int Capacity { get; }
        bool IsFull { get; }
        decimal Balance { get; }

        ICurrency StoredCurrency { get; }
        decimal StoredDenomination { get; }

        bool TryAdd(ICash[] values);
        bool TryPull(int count, out ICash[] values);
    }
}