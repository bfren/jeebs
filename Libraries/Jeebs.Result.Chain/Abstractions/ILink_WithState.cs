using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public interface ILink<TValue, TState> : ILink<TValue>
	{
		#region Map

		IR<TNext, TState> Map<TNext>(Func<IOk<TValue, TState>, IR<TNext, TState>> f);

		Task<IR<TNext, TState>> MapAsync<TNext>(Func<IOk<TValue, TState>, Task<IR<TNext, TState>>> f);

		IR<TNext, TState> Map<TNext>(Func<IOkV<TValue, TState>, IR<TNext, TState>> f);

		Task<IR<TNext, TState>> MapAsync<TNext>(Func<IOkV<TValue, TState>, Task<IR<TNext, TState>>> f);

		#endregion

		#region Run

		new IR<TValue, TState> Run(Action f);

		new Task<IR<TValue, TState>> RunAsync(Func<Task> f);

		new IR<TValue, TState> Run(Action<IOk> f);

		new Task<IR<TValue, TState>> RunAsync(Func<IOk, Task> f);

		new IR<TValue, TState> Run(Action<IOk<TValue>> f);

		new Task<IR<TValue, TState>> RunAsync(Func<IOk<TValue>, Task> f);

		new IR<TValue, TState> Run(Action<IOkV<TValue>> f);

		new Task<IR<TValue, TState>> RunAsync(Func<IOkV<TValue>, Task> f);

		IR<TValue, TState> Run(Action<IOk<TValue, TState>> f);

		Task<IR<TValue, TState>> RunAsync(Func<IOk<TValue, TState>, Task> f);

		IR<TValue, TState> Run(Action<IOkV<TValue, TState>> f);

		Task<IR<TValue, TState>> RunAsync(Func<IOkV<TValue, TState>, Task> f);

		#endregion

		#region Wrap

		new IR<TValue, TState> Wrap(TValue value);

		new IR<TValue, TState> Wrap(Func<TValue> f);

		new Task<IR<TValue, TState>> WrapAsync(Func<Task<TValue>> f);

		#endregion
	}
}
