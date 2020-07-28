using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public interface ILink
	{
		#region Map

		IR<TNext> Map<TNext>(Func<IOk, IR<TNext>> f);

		Task<IR<TNext>> MapAsync<TNext>(Func<IOk, Task<IR<TNext>>> f);

		#endregion

		#region Run

		IR Run(Action f);

		Task<IR> RunAsync(Func<Task> f);

		IR Run(Action<IOk> f);

		Task<IR> RunAsync(Func<IOk, Task> f);

		#endregion

		#region Wrap

		IR<TValue> Wrap<TValue>(TValue value);

		IR<TValue> Wrap<TValue>(Func<TValue> f);

		Task<IR<TValue>> WrapAsync<TValue>(Func<Task<TValue>> f);

		#endregion
	}
}
