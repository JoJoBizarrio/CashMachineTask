using CashMachineTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachineTask.Abstract
{
    internal interface ICash
    {
        public Guid SerialNumber { get; }

        public ICurrency Currency { get; }

        public decimal Denomination { get; }
    }
}