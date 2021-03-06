// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using System.Threading.Tasks;

namespace Jeebs.Fluent
{
	/// <summary>
	/// Add custom exception handling to the chain
	/// </summary>
	/// <typeparam name="TValue">Result / Link value type</typeparam>
	/// <typeparam name="TState">Result / Link state type</typeparam>
	/// <typeparam name="TException">Exception type</typeparam>
	public sealed class Handle<TValue, TState, TException>
		where TException : Exception
	{
		private readonly ILink<TValue, TState> link;

		internal Handle(ILink<TValue, TState> link) =>
			this.link = link;

		/// <inheritdoc cref="Handle{TValue, TException}.With(Action{IR{TValue}, TException})"/>
		public ILink<TValue, TState> With(Action<IR<TValue, TState>, TException> handler)
		{
			link.AddExceptionHandler(handler);
			return link;
		}

		/// <inheritdoc cref="Handle{TValue, TException}.With(Func{IR{TValue}, TException, Task})"/>
		public ILink<TValue, TState> With(Func<IR<TValue, TState>, TException, Task> asyncHandler)
		{
			link.AddExceptionHandler<TException>(async (r, ex) => await asyncHandler(r, ex).ConfigureAwait(false));
			return link;
		}

		/// <inheritdoc cref="Handle{TValue, TException}.With{TExceptionMsg}"/>
		public ILink<TValue, TState> With<TExceptionMsg>()
			where TExceptionMsg : IExceptionMsg, new() =>
			With((r, ex) =>
			{
				var msg = new TExceptionMsg();
				msg.Set(ex);
				r.AddMsg(msg);
			});
	}
}
