using CashMachineTask.Abstract;
using CommunityToolkit.Mvvm.Input;
namespace CashMachineTask.ViewModel
{
    internal class SelectorCashDialogViewModel : ViewModelBase, IDialogViewModel
    {
        public decimal[] SupportedDenominations { get; set; }

        private decimal _preferDenomination;
        public decimal PreferDenomination { get => _preferDenomination; set => Set(ref _preferDenomination, value); }

        public string WithdrawalSumString { get; set; }

        public object Result => PreferDenomination;
    }
}