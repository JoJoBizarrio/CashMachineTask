using System;
using CashMachineTask.Abstract;

namespace CashMachineTask.Model
{
	internal class Cash : ICash
	{
		public Guid SerialNumber { get; }
		public decimal Denomination { get; }

		public Cash(decimal denomination)
		{
			SerialNumber = Guid.NewGuid();
			Denomination = denomination;
		}
	}
}