using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachineTask.Abstract
{
	interface IDialogService
	{
		void ShowDialog<TViewModel>();
		void ShowDialog<TViewModel>(string obj);
		void ShowDialog<TViewModel>(params object[] obj);
	}
}