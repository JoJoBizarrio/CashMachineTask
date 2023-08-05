using CashMachineTask.Abstract;
using CashMachineTask.Model;
using CashMachineTask.VIewModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CashMachineTask
{
	internal class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel(ICashMachine cashMachine, IDialogService windowService)
		{
			_tray = new List<ICash>();
			_cashMachine = cashMachine;
			_windowService = windowService;
		}

		private readonly IDialogService _windowService;

		private readonly ICashMachine _cashMachine;

		private string _info;
		public string Info
		{
			get => _cashMachine.ToString();
			//set => SetProperty(ref _info, );
		}

		#region Deposit
		private readonly List<ICash> _tray;

		public decimal[] SupportedDenomination => _cashMachine.SupportedDenomination.ToArray();

		private decimal _trayCashSum;
		public decimal TrayCashSum
		{
			get => _tray.Sum(cash => cash.Denomination);
			set { }
		}

		private IRelayCommand _deposit;
		public IRelayCommand<object> Deposit
		{
			get
			{
				return (IRelayCommand<object>)(_deposit ??= new RelayCommand<object>(obj => // wtf? (IRelayCommand<decimal>) why?
				{
					_cashMachine.Deposite(_tray);
					_tray.Clear();
					RaiseCanExecuteChanged();
				},

				obj =>
				{
					return true;
				}));
			}
		}

		private IRelayCommand _pullAll;
		public IRelayCommand PullAll => _pullAll ??= new RelayCommand(() =>
		{
			_tray.Clear();
			RaiseCanExecuteChanged();
		});

		private IRelayCommand<object> _lower;
		public IRelayCommand<object> Lower => _lower ??= new RelayCommand<object>(obj =>
		{
			if (obj != null && obj is decimal denomination)
			{
				_tray.Add(new Cash(null, denomination));
				RaiseCanExecuteChanged();
			}
		});

		private IRelayCommand<object> _pickUp;
		public IRelayCommand<object> PickUp => _pickUp ??= new RelayCommand<object>(obj =>
		{
			if (obj != null && obj is decimal denomination)
			{
				_tray.Remove(_tray.First(cash => cash.Denomination == denomination));
				RaiseCanExecuteChanged();
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
			_onSelectionChangedRaiseCanExecute ??= new RelayCommand<object>(obj => { RaiseCanExecuteChanged(); });

		public void RaiseCanExecuteChanged()
		{
			PickUp.NotifyCanExecuteChanged();
			OnPropertyChanged(nameof(TrayCashSum));
			OnPropertyChanged(nameof(Info));
		}
		#endregion

		#region Withdrawal
		private string _withdrawalStatus;
		public string WithdrawalStatus
		{
			get => _withdrawalStatus;
			set => Set(ref _withdrawalStatus, value);
		}

		private string _withdrawalSum;
		public string WithdrawalSum
		{
			get => _withdrawalSum;
			set
			{
				Set(ref _withdrawalSum, value);
				Withdrawal.NotifyCanExecuteChanged();
			}
		}

		private IRelayCommand<object> _withdrawal;
		public IRelayCommand<object> Withdrawal => _withdrawal ??= new RelayCommand<object>(obj =>
		{
			if (obj is decimal preferDenomination && preferDenomination > 0)
			{
				WithdrawalStatus = "Succsesfully take: " + WithdrawalSum;
			}
		},

		obj =>
		{
			if (decimal.TryParse(WithdrawalSum, out decimal res))
			{
				return true;
			}

			return false;
		});

		private IRelayCommand _clear;
		public IRelayCommand Clear => _clear ??= new RelayCommand(() => WithdrawalSum = "");

		//private IRelayCommand _showDialog;
		//	public IRelayCommand ShowDialog => _showDialog ??= new RelayCommand(() => _windowService.ShowDialog<SelectorCashDialogViewModel>(WithdrawalSum));
		#endregion
	}
}