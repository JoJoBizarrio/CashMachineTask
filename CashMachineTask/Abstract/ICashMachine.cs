using System.Collections.Generic;

namespace CashMachineTask.Abstract
{
	internal interface ICashMachine
	{
		decimal Balance { get; }

		public IEnumerable<decimal> SupportedDenominations { get; }

		bool TryWithdrawal(decimal totalSum, out List<ICash> cashes);
		bool TryWithdrawal(decimal totalSum, decimal preferDenomination, out List<ICash> cashes);

		bool TryDeposit(IEnumerable<ICash> cash);
		bool CanDeposit(IEnumerable<ICash> cash);
	}
}