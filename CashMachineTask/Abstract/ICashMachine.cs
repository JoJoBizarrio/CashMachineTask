using System.Collections.Generic;

namespace CashMachineTask.Abstract
{
	internal interface ICashMachine
	{
		decimal Balance { get; }

		bool Deposite(IEnumerable<ICash> cash);

		IEnumerable<ICash> Withdrawal(int totalSum);

		public string GetInfo();
	}
}