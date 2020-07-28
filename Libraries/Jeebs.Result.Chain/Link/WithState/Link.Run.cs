using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		new public IR<TValue, TState> Run(Action f)
			=> result switch
			{
				IOk<TValue, TState> x => x.Catch(() => { f(); return x; }),
				_ => result.Error()
			};

		private IR<TValue, TState> PrivateRun<TResult>(Action<TResult> f)
			where TResult : IOk
			=> result switch
			{
				TResult x => x.Catch(() => { f(x); return result; }),
				_ => result.Error()
			};

		new public IR<TValue, TState> Run(Action<IOk> f)
			=> PrivateRun(f);

		new public IR<TValue, TState> Run(Action<IOk<TValue>> f)
			=> PrivateRun(f);

		new public IR<TValue, TState> Run(Action<IOkV<TValue>> f)
			=> PrivateRun(f);

		public IR<TValue, TState> Run(Action<IOk<TValue, TState>> f)
			=> PrivateRun(f);

		public IR<TValue, TState> Run(Action<IOkV<TValue, TState>> f)
			=> PrivateRun(f);
	}
}
