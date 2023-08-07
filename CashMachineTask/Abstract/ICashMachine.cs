using System.Collections.Generic;

namespace CashMachineTask.Abstract
{
	internal interface ICashMachine
	{
		decimal Balance { get; }

		string Status { get; }

		public IEnumerable<decimal> SupportedDenominations { get; }


		bool TryWithdrawalWithAnyDenomination(decimal totalSum, out List<ICash> cashes);
		bool TryWithdrawalWithPreferDenomination(decimal totalSum, decimal preferDenomination, out List<ICash> cashes);

		bool TryDeposit(IEnumerable<ICash> cash);
		bool CanDeposit(IEnumerable<ICash> cash);
	}
}