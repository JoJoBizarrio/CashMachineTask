using CashMachineTask.Abstract;
using CashMachineTask.Model;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;

namespace CashMachineTask.ViewModel
{
	internal class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel(ICashMachine cashMachine)
		{
			_tray = new List<ICash>();
			_cashMachine = cashMachine;
		}

		private readonly ICashMachine _cashMachine;

		public string Info => _cashMachine.ToString();

		public string Status => _cashMachine.Status;

		#region Deposit
		private readonly List<ICash> _tray;

		public decimal TrayCashSum => _tray.Sum(cash => cash.Denomination);

		public decimal[] SupportedDenomination => _cashMachine.SupportedDenomination.ToArray();


		private IRelayCommand<object> _deposit;
		public IRelayCommand<object> Deposit => _deposit ??= new RelayCommand<object>(obj =>
		{
			if (TrayCashSum > 0 && _cashMachine.TryDeposit(_tray))
			{
				_tray.Clear();
			}

			Notify();
		},
			obj => TrayCashSum > 0);

		private IRelayCommand _pullAll;
		public IRelayCommand PullAll => _pullAll ??= new RelayCommand(() =>
		{
			_tray.Clear();
			Notify();
		});

		private IRelayCommand<object> _lower;
		public IRelayCommand<object> Lower => _lower ??= new RelayCommand<object>(obj =>
		{
			if (obj != null && obj is decimal denomination)
			{
				_tray.Add(new Cash(denomination));
				Notify();
			}
		});

		private IRelayCommand<object> _pickUp;
		public IRelayCommand<object> PickUp => _pickUp ??= new RelayCommand<object>(obj =>
		{
			if (obj != null && obj is decimal denomination)
			{
				_tray.Remove(_tray.First(cash => cash.Denomination == denomination));
				Notify();
			}
		},

		obj =>
		{
			if (obj != null && obj is decimal denomination)
			{
				return _tray.Any(cash => cash.Denomination == denomination);
			}

			return false;
		});

		public IRelayCommand<object> _onSelectionChangedRaiseCanExecute;
		public IRelayCommand<object> OnSelectionChangedRaiseCanExecute =>
			_onSelectionChangedRaiseCanExecute ??= new RelayCommand<object>(obj => { Notify(); });

		public void Notify()
		{
			PickUp.NotifyCanExecuteChanged();
			Deposit.NotifyCanExecuteChanged();
			OnPropertyChanged(nameof(Status));
			OnPropertyChanged(nameof(TrayCashSum));
			OnPropertyChanged(nameof(Info));
		}
		#endregion

		#region Withdrawal
		private decimal _withdrawalSum;
		private string _withdrawalSumString;
		public string WithdrawalSumString
		{
			get => _withdrawalSumString;
			set
			{
				Set(ref _withdrawalSumString, value);
				Withdrawal.NotifyCanExecuteChanged();
			}
		}

		private IRelayCommand<object> _withdrawal;
		public IRelayCommand<object> Withdrawal => _withdrawal ??= new RelayCommand<object>(obj =>
		{
			if (obj is decimal preferDenomination && preferDenomination > 0)
			{
				var list = new List<ICash>();

				if (_cashMachine.TryWithdrawalWithPreferDenomination(_withdrawalSum, preferDenomination, out list))
				{
				}

				Notify();
			}
		},

		obj =>
		{
			if (decimal.TryParse(WithdrawalSumString, out decimal res))
			{
				_withdrawalSum = res;
				return true;
			}

			return false;
		});

		private IRelayCommand _clear;
		public IRelayCommand Clear => _clear ??= new RelayCommand(() => WithdrawalSumString = "");
		#endregion
	}
}