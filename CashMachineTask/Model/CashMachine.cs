using CashMachineTask.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace CashMachineTask.Model
{
	internal class CashMachine : ICashMachine
	{
		public decimal Balance => _cassettes.Sum(cassette => cassette.Balance);

		private readonly IEnumerable<ICassette> _cassettes;

		public IEnumerable<decimal> SupportedDenomination => _cassettes.Select(item => item.StoredDenomination).ToHashSet().ToList();

		public CashMachine(IEnumerable<ICassette> cassettes)
		{
			_cassettes = cassettes;
		}

		public bool Deposite(IEnumerable<ICash> cash)
		{
			//throw new NotImplementedException();
			return true;
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
			return "binded info";
		}
	}
}