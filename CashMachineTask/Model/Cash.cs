using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachineTask.Abstract;

namespace CashMachineTask.Model
{
    internal class Cash : ICash
    {
        public Guid SerialNumber { get; } = new Guid();

        public ICurrency Currency { get; }

        public decimal Denomination { get; }

        public Cash(ICurrency currency, decimal denomination)
        {
            Currency = currency;
            Denomination = denomination;
        }
    }
}