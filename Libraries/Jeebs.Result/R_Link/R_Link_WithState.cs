using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<TResult, TState>
	{
		/// <inheritdoc/>
		public IR<TResult, TState> Link(Action a) => this switch
		{
			IOk<TResult, TState> ok => Catch(() => { a(); return ok; }),
			_ => SkipAhead()
		};

		/// <inheritdoc/>
		public IR<TResult, TState> Link(Action<IOk<TResult, TState>> a) => this switch
		{
			IOk<TResult, TState> ok => Catch(() => { a(ok); return ok; }),
			_ => SkipAhead()
		};

		/// <inheritdoc/>
		public IR<TResult, TState> Link(Action<IOkV<TResult, TState>> a) => this switch
		{
			IOkV<TResult, TState> ok => Catch(() => { a(ok); return ok; }),
			_ => SkipAhead()
		};
	}
}
