using CashMachineTask.Abstract;
using CashMachineTask.Model;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CashMachineTask
{
	internal class MainWindowViewModel : ViewModelBase, IDataErrorInfo
	{
		private readonly ICashMachine _cashMachine;

		private readonly List<ICash> _tray;

		private string _info;
		public string Info
		{
			get => _cashMachine.ToString();
			set => Set(ref _info, _cashMachine.ToString());
		}

		public List<decimal> SupportedDenomination => _cashMachine.SupportedDenomination.ToList();

		public MainWindowViewModel(ICashMachine cashMachine)
		{
			_tray = new List<ICash>();
			_cashMachine = cashMachine;
		}

		private decimal _trayCashSum;
		public decimal TrayCashSum
		{
			get => _tray.Sum(cash => cash.Denomination);
			set => Set(ref _trayCashSum, value);
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

		public string Error => throw new NotImplementedException();

		public string this[string columnName]
		{
			get
			{
				string error = String.Empty;
				switch (columnName)
				{
					case "TrayCashSum":
						if (_tray.Count > 2)
						{
							error = "Возраст должен быть больше 0 и меньше 100";
						}
						break;

					case "WithdrawalSum":
						if (!decimal.TryParse(WithdrawalSum, out decimal res))
						{
							_isEnableWithdrawal = false;
						}
						else
						{
							_isEnableWithdrawal = true;
						}

						Withdrawal.NotifyCanExecuteChanged();
						break;
				}
				return error;
			}
		}

		public void RaiseCanExecuteChanged()
		{
			PickUp.NotifyCanExecuteChanged();
			OnPropertyChanged(nameof(TrayCashSum));
			OnPropertyChanged(nameof(Info));
		}

		private string _w;
		public string WithdrawalSum
		{
			get => _w;
			set => Set(ref _w, value);
		}

		private bool _isEnableWithdrawal = true;

		private IRelayCommand<object> _withdrawal;
		public IRelayCommand<object> Withdrawal => _withdrawal ??= new RelayCommand<object>(obj =>
		{

		},
			obj => { return _isEnableWithdrawal; });


	}
}