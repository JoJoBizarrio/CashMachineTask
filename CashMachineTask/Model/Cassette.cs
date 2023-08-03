using CashMachineTask.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;

namespace CashMachineTask.Model
{
	internal class Cassette<T> : ICassette<T> where T : ICash
	{
		public string Currency { get; }

		public decimal Denomination { get; }

		public int Quantity { get; private set; }

		public int Capacity { get; }

		private List<T> _storege { get; set; }

		public Cassette(T cash, int capacity)
		{
			Currency = cash.Currency.Title;
			Denomination = cash.Denomination;
			Capacity = capacity;

			_storege = new List<T>(capacity);
		}

		public bool TryAdd(T[] values)
		{
			var count = values.Length;

			if (Quantity + count <= Capacity)
			{
				foreach (T value in values)
				{
					_storege.Add(value);
				}

				Quantity += count;
				return true;
			}

			return false;
		}

		public bool TryPull(int count, out T[] values)
		{
			if (Quantity - count >= 0)
			{
				values = new T[count];

				for (int i = 0; i < count; i++)
				{
					values[i] = _storege[i];
				}

				_storege.RemoveRange(0, count);
				Quantity -= count;
				return true;
			}

			values = Array.Empty<T>();
			return false;
		}

		public string GetInfo()
		{
			throw new NotImplementedException();
		}
	}
}