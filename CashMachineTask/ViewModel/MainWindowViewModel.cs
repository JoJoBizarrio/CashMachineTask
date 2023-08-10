using CashMachineTask.Abstract;
using CashMachineTask.Model;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;
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

        private readonly List<ICash> _tray;

        #region Deposit
        public decimal TrayCashSum => _tray.Sum(cash => cash.Denomination);

        public decimal[] SupportedDenominations => _cashMachine.SupportedDenominations.ToArray();

        private IRelayCommand<object> _deposit;
        public IRelayCommand<object> Deposit => _deposit ??= new RelayCommand<object>(obj =>
        {
            if (_cashMachine.TryDeposit(_tray))
            {
                _tray.Clear();
                Status = "Deposit successfully completed!";
            }
            else
            {
                Status = "Cant deposit now. Try again or later.";
            }

            Notify();
        },
            obj => TrayCashSum > 0);

        private IRelayCommand _pullAll;
        public IRelayCommand PullAll => _pullAll ??= new RelayCommand(() =>
        {
            _tray.Clear();
            Status = "";
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

        public IRelayCommand _onSelectionChangedRaiseCanExecute;
        public IRelayCommand OnSelectionChangedRaiseCanExecute =>
            _onSelectionChangedRaiseCanExecute ??= new RelayCommand(() => { Notify(); });

        public void Notify()
        {
            PickUp.NotifyCanExecuteChanged();
            Deposit.NotifyCanExecuteChanged();
            Withdrawal.NotifyCanExecuteChanged();
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

        private IRelayCommand _withdrawal;
        public IRelayCommand Withdrawal => _withdrawal ??= new RelayCommand(() =>
        {
            if (_tray.Count > 0)
            {
                Status = "Take money from tray before withdrawal.";
                return;
            }

            var acceptedDenominations = SupportedDenominations.Where(item => item <= _withdrawalSum).ToArray();

            var dialogViewModel = new SelectorCashDialogViewModel()
            {
                SupportedDenominations = acceptedDenominations,
                WithdrawalSumString = WithdrawalSumString,
                PreferDenomination = acceptedDenominations[0]
            };

            _dialogService.ShowDialog<decimal>(dialogViewModel, (dialogResult, result) =>
            {
                if (dialogResult == false)
                {
                    Status = "Operation denied.";
                }
                else if (result is decimal preferDenomination &&
                         _cashMachine.TryWithdrawal(_withdrawalSum, preferDenomination, out var withdrawnCashList))
                {
                    _tray.AddRange(withdrawnCashList);
                    Status = "Done. Please take your money by tray.";
                    WithdrawalSumString = "";
                }
                else
                {
                    Status = "Cant do now. Try again or later";
                }
            });

            Notify();
        },

        () =>
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