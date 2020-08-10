using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		/// <inheritdoc/>
		new public ILink<TValue, TState> Handle(Action<IR<TValue>, Exception> handler)
		{
			handlers.Add<Exception>((r, ex) => handler(r, ex));
			return this;
		}

		/// <inheritdoc/>
		new public ILink<TValue, TState> Handle<TException>(Action<IR<TValue>, TException> handler)
		   where TException : Exception
		{
			handlers.Add<TException>((r, ex) => handler(r, ex));
			return this;
		}

		/// <inheritdoc/>
		new public ILink<TValue, TState> Handle(Func<IR<TValue>, Exception, Task> asyncHandler)
		{
			handlers.Add<Exception>(async (r, ex) => await asyncHandler(r, ex).ConfigureAwait(false));
			return this;
		}

		/// <inheritdoc/>
		new public ILink<TValue, TState> Handle<TException>(Func<IR<TValue>, TException, Task> asyncHandler)
			where TException : Exception
		{
			handlers.Add<TException>(async (r, ex) => await asyncHandler(r, ex).ConfigureAwait(false));
			return this;
		}

		/// <inheritdoc/>
		public ILink<TValue, TState> Handle(Action<IR<TValue, TState>, Exception> handler)
		{
			handlers.Add(handler);
			return this;
		}

		/// <inheritdoc/>
		public ILink<TValue, TState> Handle<TException>(Action<IR<TValue, TState>, TException> handler)
			where TException : Exception
		{
			handlers.Add(handler);
			return this;
		}

		/// <inheritdoc/>
		public ILink<TValue, TState> Handle(Func<IR<TValue, TState>, Exception, Task> asyncHandler)
		{
			handlers.Add<Exception>(async (r, ex) => await asyncHandler(r, ex).ConfigureAwait(false));
			return this;
		}

		/// <inheritdoc/>
		public ILink<TValue, TState> Handle<TException>(Func<IR<TValue, TState>, TException, Task> asyncHandler)
			where TException : Exception
		{
			handlers.Add<TException>(async (r, ex) => await asyncHandler(r, ex).ConfigureAwait(false));
			return this;
		}
	}
}
