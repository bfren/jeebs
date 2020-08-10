using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		/// <inheritdoc/>
		public ILink<TValue> Handle(Action<IR<TValue>, Exception> handler)
		{
			handlers.Add(handler);
			return this;
		}

		/// <inheritdoc/>
		public ILink<TValue> Handle<TException>(Action<IR<TValue>, TException> handler)
			where TException : Exception
		{
			handlers.Add(handler);
			return this;
		}

		/// <inheritdoc/>
		public ILink<TValue> Handle(Func<IR<TValue>, Exception, Task> asyncHandler)
		{
			handlers.Add<Exception>(async (r, ex) => await asyncHandler(r, ex).ConfigureAwait(false));
			return this;
		}

		/// <inheritdoc/>
		public ILink<TValue> Handle<TException>(Func<IR<TValue>, TException, Task> asyncHandler)
			where TException : Exception
		{
			handlers.Add<TException>(async (r, ex) => await asyncHandler(r, ex).ConfigureAwait(false));
			return this;
		}
	}
}
