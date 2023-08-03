using CashMachineTask.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashMachineTask.Model
{
	internal class CashMachine : ICashMachine
	{
		public decimal Balance => _cassettes.Sum(cassette => cassette.Balance);

		private readonly int _cassettesDefultMaxCount = 4;
		private readonly ICassette[] _cassettes;
		private readonly IDictionary<ICurrency, decimal[]> _supportedCash;
		private readonly decimal[] _supDenom;

		public CashMachine(IDictionary<ICurrency, decimal[]> currencyKeyDenominationsValuesDictionary, int cassetteCapacity) // heavy ctor, maybe factory?
		{
			_cassettes = new ICassette[_cassettesDefultMaxCount];
			_supportedCash = currencyKeyDenominationsValuesDictionary;

			var supportedCurrency = _supportedCash.Keys.ToArray();

			for (int i = 0; i < _cassettesDefultMaxCount; i++)
			{
				for (int j = 0; j < _supportedCash[supportedCurrency[i]].Length; j++)
				{
					_cassettes[i] = new Cassette(supportedCurrency[i], _supportedCash[supportedCurrency[i]][j], cassetteCapacity);
				}
			}
		}

		public bool Deposite(IEnumerable<ICash> cash)
		{
			throw new NotImplementedException();

		}

		private bool Deposite(decimal denomination, int count)
		{
			throw new NotImplementedException();

		}

		public IEnumerable<ICash> Withdrawal(int totalSum)
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}