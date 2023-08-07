using CashMachineTask.Abstract;
using CashMachineTask.Model;
using CashMachineTask.View;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CashMachineTask.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ICashMachine cashMachine, IDialogService dialogService)
        {
            _tray = new List<ICash>();
            _cashMachine = cashMachine;
            _dialogService = dialogService;
        }

        private readonly ICashMachine _cashMachine;
        private readonly IDialogService _dialogService;

        public string Info => _cashMachine.ToString();

        private string _status;
        public string Status { get => _status; set => Set(ref _status, value); }

        #region Deposit
        private readonly List<ICash> _tray;

        public decimal TrayCashSum => _tray.Sum(cash => cash.Denomination);

        public decimal[] SupportedDenominations => _cashMachine.SupportedDenominations.ToArray();

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
            Status = _cashMachine.Status;
            OnPropertyChanged(nameof(TrayCashSum));
            OnPropertyChanged(nameof(Info));
        }
        #endregion

        #region Withdrawal
        private decimal _withdrawalSum;
        private string _withdrawalSumString = "";
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
            DialogParametrs dialogParametrs = new DialogParametrs();
            dialogParametrs.Register(nameof(WithdrawalSumString), WithdrawalSumString);
            dialogParametrs.Register(nameof(SupportedDenominations), SupportedDenominations);
            dialogParametrs.Register<Window>("Owner", obj);

            var list = new List<ICash>();

            if (_tray.Count == 0)
            {
                _dialogService.ShowDialog<SelectorCashDialogViewModel>(dialogParametrs, (dialogResult, result) =>
                {
                    if (dialogResult == true && result is decimal preferDenomination &&
                    _cashMachine.TryWithdrawalWithPreferDenomination(_withdrawalSum, preferDenomination, out list))
                    {
                        _tray.AddRange(list);
                    }
                });

                Notify();
            }
            else
            {
                Status = "Take money from tray before withdrawal.";
            }
        },

        obj =>
        {
            if (decimal.TryParse(WithdrawalSumString, out decimal res) && res >= SupportedDenominations.Min())
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