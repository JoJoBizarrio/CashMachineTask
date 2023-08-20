using CashMachineTask.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashMachineTask.Model
{
	internal class Cassette : ICassette
	{
		public decimal StoredDenomination { get; }
		public int Quantity => _storege.Count;
		public int Capacity => _storege.Capacity;
		public decimal Balance => Quantity * StoredDenomination;

		private List<ICash> _storege { get; set; }

		public Cassette(decimal denomination, int quantity, int capacity)
		{
			if (quantity > capacity)
			{
				throw new ArgumentException("Cashes quantity is more than capacity of cassette.", nameof(quantity));
			}

			_storege = new List<ICash>(capacity);

			for (int i = 0; i < quantity; i++)
			{
				_storege.Add(new Cash(denomination));
			}

			StoredDenomination = denomination;
		}

		public bool CanDeposit(IEnumerable<ICash> cashes)
		{
			if (cashes.Any(cash => cash.Denomination != StoredDenomination))
			{
				return false;
			}

			if (Quantity + cashes.Count() > Capacity)
			{
				return false;
			}

			return true;
		}

		public void Deposite(IEnumerable<ICash> cashes)
		{
			if (!CanDeposit(cashes))
			{
				throw new InvalidOperationException("Cashes is wrong.");
			}

			_storege.AddRange(cashes);
		}

		public IEnumerable<ICash> Withdrawal(int cashesCount)
		{
			var list = new List<ICash>(cashesCount);

			for (int i = 0; i < cashesCount; i++)
			{
				list.Add(_storege[i]);
			}

			_storege.RemoveRange(0, cashesCount);
			return list;
		}
	}
}