using CashMachineTask.Model;
using System;

namespace CashMachineTask.Abstract
{
    internal interface ICash
    {
        public Guid SerialNumber { get; }

        public ICurrency Currency { get; }

        public decimal Denomination { get; }
    }
}