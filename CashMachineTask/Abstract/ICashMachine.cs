using System.Collections.Generic;

namespace CashMachineTask.Abstract
{
    internal interface ICashMachine
    {
        decimal Cash { get; }
        IEnumerable<ICurrency> SupportedCurrencies { get; }

        int CassettesMaxCount { get; }
        IEnumerable<ICassette<ICurrency>> Cassettes { get; }

        bool Deposite(IEnumerable<ICurrency> banknotes);

        IEnumerable<ICurrency> Withdrawal(int totalSum);
    }
}