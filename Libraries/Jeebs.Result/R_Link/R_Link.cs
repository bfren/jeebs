using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public abstract partial class R<TResult> : IR<TResult>
	{
		/// <inheritdoc/>
		public IR<TResult> Link(Action a) => this switch
		{
			IOk<TResult> ok => Catch(() => { a(); return ok; }),
			_ => SkipAhead()
		};

		/// <inheritdoc/>
		public IR<TResult> Link(Action<IOk<TResult>> a) => this switch
		{
			IOk<TResult> ok => Catch(() => { a(ok); return ok; }),
			_ => SkipAhead()
		};

		/// <inheritdoc/>
		public IR<TResult> Link(Action<IOkV<TResult>> a) => this switch
		{
			IOkV<TResult> ok => Catch(() => { a(ok); return ok; }),
			_ => SkipAhead()
		};
	}
}
