using System.Collections;

namespace CashMachineTask.Abstract
{
	internal interface ICassette<T>
	{
		int Quantity { get; }

		int Capacity { get; }

		bool TryAdd(T[] values);

		bool TryPull(int count, out T[] values);
	}
}