using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<TResult> : IR<TResult>
	{
		/// <inheritdoc/>
		public IR<TNext> LinkMap<TNext>(Func<TNext> f) => this switch
		{
			IOk<TResult> ok => Catch(() => { var v = f(); return ok.OkV(v); }),
			_ => SkipAhead<TNext>()
		};

		/// <inheritdoc/>
		public IR<TNext> LinkMap<TNext>(Func<IOk<TResult>, IR<TNext>> f) => this switch
		{
			IOk<TResult> s => Catch(() => f(s)),
			_ => SkipAhead<TNext>()
		};

		/// <inheritdoc/>
		public IR<TNext> LinkMap<TNext>(Func<IOkV<TResult>, IR<TNext>> f) => this switch
		{
			IOkV<TResult> s => Catch(() => f(s)),
			_ => SkipAhead<TNext>()
		};
	}
}
