using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<TResult, TState> : IR<TResult, TState>
	{
		/// <inheritdoc/>
		public IR<TNext, TState> LinkMap<TNext>(Func<TNext> f) => this switch
		{
			IOk<TResult, TState> ok => Catch(() => { var v = f(); return ok.OkV(v); }),
			_ => SkipAhead<TNext>()
		};

		/// <inheritdoc/>
		public IR<TNext, TState> LinkMap<TNext>(Func<IOk<TResult, TState>, IR<TNext, TState>> f) => this switch
		{
			IOk<TResult, TState> s => Catch(() => f(s)),
			_ => SkipAhead<TNext>()
		};

		/// <inheritdoc/>
		public IR<TNext, TState> LinkMap<TNext>(Func<IOkV<TResult, TState>, IR<TNext, TState>> f) => this switch
		{
			IOkV<TResult, TState> s => Catch(() => f(s)),
			_ => SkipAhead<TNext>()
		};
	}
}
