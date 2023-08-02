using CashMachineTask.Abstract;
using System;
using System.Collections.Generic;

namespace CashMachineTask.Model
{
    internal class CashMachine : ICashMachine
    {
        public decimal Cash { get; private set; }
        public IEnumerable<ICurrency> SupportedCurrencies => throw new NotImplementedException();

        public int CassettesMaxCount => throw new NotImplementedException();
        public IEnumerable<ICassette<ICurrency>> Cassettes => throw new NotImplementedException();

        public CashMachine()
        {

        }

        public bool Deposite(IEnumerable<ICurrency> banknotes)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICurrency> Withdrawal(int totalSum)
        {
            throw new NotImplementedException();
        }
    }
}
