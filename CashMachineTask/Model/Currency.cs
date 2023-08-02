using CashMachineTask.Abstract;

namespace CashMachineTask.Model
{
	public class Currency
	{
		public string Title { get; }

		public Currency(string title)
		{
			Title = title;
		}
	}
}