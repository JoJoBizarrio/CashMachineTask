using CashMachineTask.Abstract;
using CashMachineTask.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CashMachineTask.Model
{
	class DialogService : IDialogService
	{
		private static Dictionary<Type, Type> _mapping = new Dictionary<Type, Type>();

		public static void RegisterDialog<TViewModel, TView>()
		{
			_mapping.Add(typeof(TViewModel), typeof(TView));
		}

		public void ShowDialog<TViewModel>()
		{
			var viewType = _mapping[typeof(TViewModel)];
			var item = Activator.CreateInstance(viewType);

			if (item is Window dialog)
			{
				dialog.DataContext = Activator.CreateInstance(typeof(TViewModel));
				dialog.ShowDialog();
			}
		}

		public void ShowDialog<TViewModel>(params object[] args)
		{
			var viewType = _mapping[typeof(TViewModel)];
			var item = Activator.CreateInstance(viewType);

			if (item is Window dialog)
			{
				dialog.DataContext = Activator.CreateInstance(typeof(TViewModel), args);
				dialog.ShowDialog();
			}
		}

		public void ShowDialog<TViewModel>(string args)
		{
			var viewType = _mapping[typeof(TViewModel)];
			var item = Activator.CreateInstance(viewType);

			if (item is Window dialog)
			{
				dialog.DataContext = Activator.CreateInstance(typeof(TViewModel), args);
				dialog.ShowDialog();
			}
		}
	}
}