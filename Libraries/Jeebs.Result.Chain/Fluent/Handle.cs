using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Fluent
{
	/// <summary>
	/// Add custom exception handling to the chain
	/// </summary>
	/// <typeparam name="TValue">Result / Link value type</typeparam>
	/// <typeparam name="TException">Exception type</typeparam>
	public sealed class Handle<TValue, TException>
		where TException : Exception
	{
		private readonly ILink<TValue> link;

		internal Handle(ILink<TValue> link) =>
			this.link = link;

		/// <summary>
		/// Add an exception handler to the current link
		/// </summary>
		/// <param name="handler">Exception handler</param>
		public ILink<TValue> With(Action<IR<TValue>, TException> handler)
		{
			link.AddExceptionHandler(handler);
			return link;
		}

		/// <summary>
		/// Add an asynchronous exception handler to the current link
		/// </summary>
		/// <param name="asyncHandler">Asynchronous exception handler</param>
		public ILink<TValue> With(Func<IR<TValue>, TException, Task> asyncHandler)
		{
			link.AddExceptionHandler<TException>(async (r, ex) => await asyncHandler(r, ex).ConfigureAwait(false));
			return link;
		}

		/// <summary>
		/// Handle all exceptions using the specified <typeparamref name="TExceptionMsg"/>
		/// </summary>
		/// <typeparam name="TExceptionMsg">Exception message type</typeparam>
		public ILink<TValue> With<TExceptionMsg>()
			where TExceptionMsg : IExceptionMsg, new() =>
			With((r, ex) =>
			{
				var msg = new TExceptionMsg();
				msg.Set(ex);
				r.AddMsg(msg);
			});
	}
}
