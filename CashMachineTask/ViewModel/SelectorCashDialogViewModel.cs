using CashMachineTask.Abstract;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace CashMachineTask.VIewModel
{
	internal class SelectorCashDialogViewModel : ViewModelBase
	{
		public SelectorCashDialogViewModel()
		{

		}

		public SelectorCashDialogViewModel(string str)
		{
			WithdrawalSum = str;
		}

		public SelectorCashDialogViewModel(params object[] list)
		{
			WithdrawalSum = (string)list[0];
			SupportedDenomination = (decimal[])list[1];
		}

		public string WithdrawalSum { get; set; }

		public decimal[] SupportedDenomination { get; set; }



	}
}