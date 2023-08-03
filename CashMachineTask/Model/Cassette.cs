using CashMachineTask.Abstract;
using System;
using System.Collections.Generic;

namespace CashMachineTask.Model
{
    internal class Cassette : ICassette
    {
        public ICurrency StoredCurrency { get; }
        public decimal StoredDenomination { get; }

        public int Quantity { get; private set; }
        public int Capacity { get; }
        public bool IsFull => Quantity == Capacity;
        public decimal Balance => Quantity * StoredDenomination;

        private List<ICash> _storege { get; set; }

        public Cassette(ICurrency currency, decimal denomination, int capacity)
        {
            Capacity = capacity;
            StoredCurrency = currency;
            StoredDenomination = denomination;
            _storege = new List<ICash>(capacity);
        }

        public bool TryAdd(ICash[] values)
        {
            var count = values.Length;

            if (Quantity + count <= Capacity)
            {
                foreach (Cash value in values)
                {
                    _storege.Add(value);
                }

                Quantity += count;
                return true;
            }

            return false;
        }

        public bool TryPull(int count, out ICash[] values)
        {
            if (Quantity - count >= 0)
            {
                values = new Cash[count];

                for (int i = 0; i < count; i++)
                {
                    values[i] = _storege[i];
                }

                _storege.RemoveRange(0, count);
                Quantity -= count;
                return true;
            }

            values = Array.Empty<Cash>();
            return false;
        }
    }
}