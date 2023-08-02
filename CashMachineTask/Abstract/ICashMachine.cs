using System.Collections.Generic;

namespace CashMachineTask.Abstract
{
    internal interface ICashMachine
    {
        decimal Cash { get; }

        int CassettesMaxCount { get; }

        IEnumerable<ICassette> Cassettes { get; }

        IEnumerable<ICurrency> SupportedCurrencies { get; }

        bool Deposite(IEnumerable<ICurrency> banknotes);

        IEnumerable<ICurrency> Withdrawal(int totalSum);
    }
}