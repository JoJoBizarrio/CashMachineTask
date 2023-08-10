using CashMachineTask.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashMachineTask.Model
{
    internal class CashMachine : ICashMachine
    {
        public decimal Balance => _cassettes.Sum(cassette => cassette.Balance);

        public IEnumerable<decimal> SupportedDenominations => _cassettes.Select(item => item.StoredDenomination).OrderBy(x => x).ToHashSet().ToArray();

        private readonly List<ICassette> _cassettes;

        public CashMachine(IEnumerable<ICassette> cassettes)
        {
            if (cassettes == null)
            {
                throw new ArgumentNullException("Cassettes is null");
            }

            if (cassettes.Count() == 0)
            {
                throw new ArgumentException("Cassettes is empty");
            }

            if (cassettes.GroupBy(item => item.StoredDenomination).ToArray().Count() != cassettes.Count())
            {
                throw new ArgumentException("One cassette = One denomination");
            }

            _cassettes = cassettes.ToList();
        }

        #region deposit
        public bool TryDeposit(IEnumerable<ICash> cash)
        {
            var cashGroupByDenomination = cash.GroupBy(item => item.Denomination).ToArray();

            if (!CanDeposit(cashGroupByDenomination))
            {
                return false;
            };

            for (int i = 0; i < cashGroupByDenomination.Length; i++)
            {
                _cassettes.First(item => item.StoredDenomination == cashGroupByDenomination[i].Key).Deposite(cashGroupByDenomination[i]);
            }

            return true;
        }

        public bool CanDeposit(IEnumerable<ICash> cash)
        {
            var cashGroupByDenomination = cash.GroupBy(item => item.Denomination).ToArray();

            for (int i = 0; i < cashGroupByDenomination.Length; i++)
            {
                if (!_cassettes.First(item => item.StoredDenomination == cashGroupByDenomination[i].Key).CanDeposit(cashGroupByDenomination[i]))
                {
                    return false;
                };
            }

            return true;
        }

        private bool CanDeposit(IGrouping<decimal, ICash>[] cashGroupByDenomination)
        {
            return cashGroupByDenomination.Select(item => CanDeposit(item)).All(item => item);
        }

        private bool CanDeposit(IGrouping<decimal, ICash> cashGroupByDenomination)
        {
            return _cassettes.First(item => item.StoredDenomination == cashGroupByDenomination.Key).CanDeposit(cashGroupByDenomination);
        }
        #endregion

        #region withdrawal
        public bool TryWithdrawal(decimal totalSum, out List<ICash> cashes)
        {
            return TryWithdrawal(totalSum, SupportedDenominations.ToArray(), out cashes);
        }

        public bool TryWithdrawal(decimal totalSum, decimal preferDenomination, out List<ICash> cashes)
        {
            var denomination = SupportedDenominations.Where(item => item <= preferDenomination).OrderByDescending(item => item).ToArray();

            return TryWithdrawal(totalSum, denomination, out cashes);
        }

        private bool TryWithdrawal(decimal totalSum, decimal[] denomination, out List<ICash> cashes)
        {
            if (totalSum > Balance)
            {
                cashes = null;
                return false;
            }

            var length = denomination.Length;
            var cashesCount = 0;
            var sumResidual = totalSum;
            var denominationKeyCountValueDictionary = new Dictionary<decimal, int>();
            ICassette cassette;

            for (int i = 0; i < length; i++)
            {
                cashesCount = 0;
                cashesCount = (int)(sumResidual / denomination[i]);

                cassette = _cassettes.First(item => item.StoredDenomination == denomination[i]);

                if (cassette.Quantity >= cashesCount)
                {
                    denominationKeyCountValueDictionary.Add(denomination[i], cashesCount);
                    sumResidual -= denomination[i] * cashesCount;
                }
                else
                {
                    denominationKeyCountValueDictionary.Add(denomination[i], cassette.Quantity);
                    sumResidual -= denomination[i] * cassette.Quantity;
                }
            }

            if (sumResidual > 0)
            {
                cashes = null;
                return false;
            }

            cashes = new List<ICash>();

            foreach (var key in denominationKeyCountValueDictionary)
            {
                cashes.AddRange(_cassettes.First(item => item.StoredDenomination == key.Key).Withdrawal(key.Value));
            }

            return true;
        }
        #endregion

        #region by object
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Total balance: ");
            stringBuilder.AppendLine(Balance.ToString());
            stringBuilder.AppendLine();

            stringBuilder.AppendLine("Balance by denomination:");

            var cassettesCount = _cassettes.Count();

            for (int i = 0; i < cassettesCount; i++)
            {
                stringBuilder.AppendLine(_cassettes[i].ToString());
            }

            return stringBuilder.ToString();
        }
        #endregion
    }
}