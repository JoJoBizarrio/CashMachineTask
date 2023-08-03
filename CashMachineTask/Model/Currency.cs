using CashMachineTask.Abstract;

namespace CashMachineTask.Model
{
    public class Currency : ICurrency
    {
        public string Title { get; }

        public Currency(string title)
        {
            Title = title;
        }
    }
}