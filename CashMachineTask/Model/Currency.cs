using CashMachineTask.Abstract;

namespace CashMachineTask.Model
{
    public class Currency : ICurrency
    {
        public string Title { get; }

        public decimal Denomination { get; }

        public Currency(string title, decimal denomination)
        {
            Title = title;
            Denomination = denomination;
        }
    }
}
