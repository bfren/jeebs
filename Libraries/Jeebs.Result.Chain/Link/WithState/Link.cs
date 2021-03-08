// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <inheritdoc cref="ILink{TValue, TState}"/>
	public partial class Link<TValue, TState> : Link<TValue>, ILink<TValue, TState>
	{
		private readonly IR<TValue, TState> result;

		private readonly LinkExceptionHandlers<IR<TValue, TState>> handlers = new();

		internal Link(IR<TValue, TState> result, Func<Exception, IMsg>? exceptionMsg = null) : base(result)
		{
			this.result = result;
			handlers = new LinkExceptionHandlers<IR<TValue, TState>>(exceptionMsg);
		}

		/// <inheritdoc/>
		public void AddExceptionHandler<TException>(Action<IR<TValue, TState>, TException> handler)
			where TException : Exception =>
			handlers.Add(handler);

		/// <inheritdoc/>
		new public void AddExceptionHandler<TException>(Action<IR<TValue>, TException> handler)
			where TException : Exception =>
			handlers.Add(handler);

		private IR<TNext, TState> Catch<TNext>(Func<Task<IR<TNext, TState>>> f)
		{
			try
			{
				return f().Await();
			}
			catch (Exception ex)
			{
				result.Logger.Error(ex, "Link Error - check Exception for details");
				handlers.Handle(result, ex);
				return result.Error<TNext>();
			}
		}

		private IR<TNext, TState> Catch<TNext>(Func<IR<TNext, TState>> f) =>
			Catch(() => Task.FromResult(f()));

		/// <inheritdoc cref="Link{TValue}.Dispose"/>
		new public void Dispose()
		{
			handlers.Dispose();
			base.Dispose();
		}
	}
}
