using CashMachineTask.Abstract;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CashMachineTask.ViewModel
{
    internal class SelectorCashDialogViewModel : ViewModelBase, IDialogViewModel
    {
        public decimal[] SupportedDenominations { get; set; }

        private decimal _preferDenomination;
        public decimal PreferDenomination { get => _preferDenomination; set => Set(ref _preferDenomination, value); }

        public string WithdrawalSumString { get; set; }

        public object Result => PreferDenomination;

        public void OnDialogOpened(IDialogParametrs parametrs)
        {
            SupportedDenominations = parametrs.GetValue<decimal[]>(nameof(SupportedDenominations)); // how cut nameof??
            WithdrawalSumString = parametrs.GetValue<string>(nameof(WithdrawalSumString));
            PreferDenomination = SupportedDenominations.Min();
        }
    }
}