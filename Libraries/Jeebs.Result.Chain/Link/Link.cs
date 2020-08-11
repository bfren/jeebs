using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jm.Link;

namespace Jeebs
{
	/// <inheritdoc cref="ILink{TValue}"/>
	public partial class Link<TValue> : ILink<TValue>
	{
		private readonly IR<TValue> result;

		private readonly LinkExceptionHandlers<IR<TValue>> handlers = new LinkExceptionHandlers<IR<TValue>>();

		internal Link(IR result, Func<Exception, IMsg>? exceptionMsg = null) : this(result.ChangeType().To<TValue>(), exceptionMsg) { }

		internal Link(IR<TValue> result, Func<Exception, IMsg>? exceptionMsg = null)
		{
			this.result = result;
			handlers = new LinkExceptionHandlers<IR<TValue>>(exceptionMsg);
		}

		/// <inheritdoc/>
		public void AddExceptionHandler<TException>(Action<IR<TValue>, TException> handler)
			where TException : Exception
			=> handlers.Add(handler);

		private IR<TNext> Catch<TNext>(Func<Task<IR<TNext>>> f)
		{
			try
			{
				return f().Await();
			}
			catch (Exception ex)
			{
				handlers.Handle(result, ex);
				return result.Error<TNext>();
			}
		}

		private IR<TNext> Catch<TNext>(Func<IR<TNext>> f)
			=> Catch(() => Task.FromResult(f()));

		/// <summary>
		/// Dispose of this Link - including result and handlers objects
		/// </summary>
		public void Dispose()
		{
			handlers.Dispose();
			result.Dispose();
		}
	}
}
