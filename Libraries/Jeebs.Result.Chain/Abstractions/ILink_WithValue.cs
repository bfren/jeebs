using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public interface ILink<TValue> : ILink
	{
		#region Map

		IR<TNext> Map<TNext>(Func<IOk<TValue>, IR<TNext>> f);

		Task<IR<TNext>> MapAsync<TNext>(Func<IOk<TValue>, Task<IR<TNext>>> f);

		IR<TNext> Map<TNext>(Func<IOkV<TValue>, IR<TNext>> f);

		Task<IR<TNext>> MapAsync<TNext>(Func<IOkV<TValue>, Task<IR<TNext>>> f);

		#endregion

		#region Run

		new IR<TValue> Run(Action f);

		new Task<IR<TValue>> RunAsync(Func<Task> f);

		new IR<TValue> Run(Action<IOk> f);

		new Task<IR<TValue>> RunAsync(Func<IOk, Task> f);

		IR<TValue> Run(Action<IOk<TValue>> f);

		Task<IR<TValue>> RunAsync(Func<IOk<TValue>, Task> f);

		IR<TValue> Run(Action<IOkV<TValue>> f);

		Task<IR<TValue>> RunAsync(Func<IOkV<TValue>, Task> f);

		#endregion

		#region Wrap

		IR<TValue> Wrap(TValue value);

		IR<TValue> Wrap(Func<TValue> f);

		Task<IR<TValue>> WrapAsync(Func<Task<TValue>> f);

		#endregion
	}
}
