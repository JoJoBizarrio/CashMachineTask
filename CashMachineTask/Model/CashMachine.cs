using CashMachineTask.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashMachineTask.Model
{
	internal class CashMachine : ICashMachine
	{
		public decimal Cash { get; private set; }

		private readonly int _cassettesDefultMaxCount = 4;
		private readonly ICassette<ICash>[] _cassettes;
		private readonly ICash[] _supportedCash;

		public CashMachine(IEnumerable<ICash> supportedCash, int cassetteCapacity)
		{
			_cassettes = new ICassette<ICash>[_cassettesDefultMaxCount];
			_supportedCash = supportedCash.ToArray();

			for (int i = 0; i < _cassettesDefultMaxCount; i++)
			{
				_cassettes[i] = new Cassette<ICash>(_supportedCash[i], cassetteCapacity);
			}
		}

		public bool Deposite(IEnumerable<ICash> cash)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ICash> Withdrawal(int totalSum)
		{
			throw new NotImplementedException();
		}
	}
}