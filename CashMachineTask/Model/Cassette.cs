using CashMachineTask.Abstract;
using System;

namespace CashMachineTask.Model
{
    internal class Cassette : ICassette<ICurrency>
    {
        public ICurrency StoredCurrency { get; }

        public int Quantity { get; }

        public int Capacity { get; }

        public bool AddBanknots(int currencyCount)
        {
            throw new NotImplementedException();
        }

        public Cassette(ICurrency currency)
        {
            StoredCurrency = currency;
        }
    }
}
