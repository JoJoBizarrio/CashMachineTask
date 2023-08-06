using CashMachineTask.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashMachineTask.Model
{
	internal class CashMachine : ICashMachine
	{
		public decimal Balance => _cassettes.Sum(cassette => cassette.Balance);

		public IEnumerable<decimal> SupportedDenomination => _cassettes.Select(item => item.StoredDenomination).OrderBy(x => x).ToHashSet().ToArray();

		public string Status { get; private set; }


		private readonly List<ICassette> _cassettes;

		public CashMachine(IEnumerable<ICassette> cassettes)
		{
			if (cassettes.GroupBy(item => item.StoredDenomination).ToArray().Count() != cassettes.Count())
			{
				throw new ArgumentException("One cassette = One denomination");
			}

			_cassettes = cassettes.ToList();
		}

		#region deposit
		public bool TryDeposit(IEnumerable<ICash> cash)
		{
			var cashGroupByDenomination = cash.GroupBy(item => item.Denomination).ToArray();

			if (!CanDeposit(cashGroupByDenomination))
			{
				Status = "Cant deposit. Try again or later";
				return false;
			};

			for (int i = 0; i < cashGroupByDenomination.Length; i++)
			{
				_cassettes.First(item => item.StoredDenomination == cashGroupByDenomination[i].Key).Deposite(cashGroupByDenomination[i]);
			}

			Status = "Done. Cashes deposited on your balance";
			return true;
		}

		public bool CanDeposit(IEnumerable<ICash> cash)
		{
			var cashGroupByDenomination = cash.GroupBy(item => item.Denomination).ToArray();

			for (int i = 0; i < cashGroupByDenomination.Length; i++)
			{
				if (!_cassettes.First(item => item.StoredDenomination == cashGroupByDenomination[i].Key).CanDeposit(cashGroupByDenomination[i]))
				{
					return false;
				};
			}

			return true;
		}

		private bool CanDeposit(IGrouping<decimal, ICash>[] cashGroupByDenomination)
		{
			return cashGroupByDenomination.Select(item => CanDeposit(item)).All(item => item);
		}

		private bool CanDeposit(IGrouping<decimal, ICash> cashGroupByDenomination)
		{
			return _cassettes.First(item => item.StoredDenomination == cashGroupByDenomination.Key).CanDeposit(cashGroupByDenomination);
		}
		#endregion

		#region withdrawal
		public bool TryWithdrawalWithAnyDenomination(decimal totalSum, out List<ICash> cashes)
		{
			return TryWithdrawal(totalSum, SupportedDenomination.ToArray(), out cashes);
		}

		public bool TryWithdrawalWithPreferDenomination(decimal totalSum, decimal preferDenomination, out List<ICash> cashes)
		{
			cashes = new List<ICash>();

			var denomination = SupportedDenomination.Where(item => item <= preferDenomination).OrderByDescending(item => item).ToArray();

			return TryWithdrawal(totalSum, denomination, out cashes);
		}

		private bool TryWithdrawal(decimal totalSum, decimal[] preferDenomination, out List<ICash> cashes)
		{
			cashes = new List<ICash>();

			if (totalSum > Balance)
			{
				Status = "Something went wrong. Try again or later.";
				return false;
			}

			var length = preferDenomination.Length;
			var cashCount = 0;
			var current = totalSum;
			var denominationKeyCountValueDictionary = new Dictionary<decimal, int>();

			for (int i = 0; i < length; i++)
			{
				cashCount = 0;
				cashCount = (int)(current / preferDenomination[i]);
				current -= preferDenomination[i] * cashCount;

				if (_cassettes.First(item => item.StoredDenomination == preferDenomination[i]).Quantity >= cashCount)
				{
					denominationKeyCountValueDictionary.Add(preferDenomination[i], cashCount);
				}
				else
				{
					cashes = null;
					Status = "Cant do now. Try again or later";
					return false;
				}
			}

			foreach (var key in denominationKeyCountValueDictionary)
			{
				cashes.AddRange(_cassettes.First(item => item.StoredDenomination == key.Key).Withdrawal(key.Value));
				Status = "Take your money";
			}

			return true;
		}
		#endregion

		#region by object
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();

			stringBuilder.Append("Total balance: ");
			stringBuilder.AppendLine(Balance.ToString());
			stringBuilder.AppendLine();

			stringBuilder.AppendLine("Balance by denomination:");

			var cassettesCount = _cassettes.Count();

			for (int i = 0; i < cassettesCount; i++)
			{
				stringBuilder.AppendLine(_cassettes[i].ToString());
			}

			return stringBuilder.ToString();
		}
		#endregion
	}
}