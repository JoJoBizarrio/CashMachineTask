using CashMachineTask.Abstract;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace CashMachineTask
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly ICashMachine _cashMachine;

        private string _info;
        public string Info
        {
            get => _info;
            set => Set(ref _info, _cashMachine.ToString());
        }

        public List<decimal> SupportedDenomination => _cashMachine.SupportedDenomination.ToList();

        public MainWindowViewModel(ICashMachine cashMachine)
        {
            _cashMachine = cashMachine;
            OnPropertyChanged(nameof(Info));
        }

        private IRelayCommand _deposite;

        public IRelayCommand<decimal> Deposite
        {
            get
            {
                return (IRelayCommand<decimal>)(_deposite ??= new RelayCommand<decimal>(input => // wtf? (IRelayCommand<decimal>) why?
                {
                    throw new NotImplementedException(); //_cashMachine.Deposite(input);
                },
                input =>
                {
                    return false;
                }));
            }
        }
    }
}