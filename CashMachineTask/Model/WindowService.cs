using CashMachineTask.Abstract;
using CashMachineTask.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachineTask.Model
{
	class WindowsService : IWindowService
	{
		public void OpenSelectorCashDialog(object viewModel)
		{
			var selector = new SelectorCashDialog();
			selector.DataContext = viewModel;
		}
	}
}