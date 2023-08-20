using System.Collections.Generic;

namespace CashMachineTask.Abstract
{
	internal interface ICassette
	{
		int Quantity { get; }
		int Capacity { get; }
		decimal Balance { get; }
		decimal StoredDenomination { get; }

		bool CanDeposit(IEnumerable<ICash> cashes);
		void Deposite(IEnumerable<ICash> cashes);

		IEnumerable<ICash> Withdrawal(int cashesCount);
	}
}