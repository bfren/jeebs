using System;
using System.Collections.Generic;
using System.Text;
using Jm.Link;

namespace Jeebs
{
	internal sealed class LinkExceptionHandlers<TResult> : IDisposable
		where TResult : IR
	{
		private readonly Dictionary<Type, Action<TResult, Exception>> handlers;

		private readonly Func<Exception, IMsg> exceptionMsg;

		internal LinkExceptionHandlers(Func<Exception, IMsg>? exceptionMsg = null)
		{
			handlers = new Dictionary<Type, Action<TResult, Exception>>();
			this.exceptionMsg = exceptionMsg ?? (ex => new LinkExceptionMsg(ex));
		}

		internal void Add<TException>(Action<TResult, TException> handler)
			where TException : Exception =>
			handlers[typeof(TException)] = (r, ex) =>
			{
				if (ex is TException t)
				{
					handler(r, t);
				}
			};

		private Action<TResult, Exception>? Get(Type ex) =>
			handlers.TryGetValue(ex, out var value) ? value : null;

		internal void Handle(TResult result, Exception ex)
		{
			// Work out which handler to use:
			//   Specific (for this particular exception)
			//   Generic (custom-defined for any exception)
			//   Fallback (in case nothing is defined)
			var handle = handlers.Count switch
			{
				0 =>
					fallback,

				_ =>
					Get(ex.GetType()) switch
					{
						{ } specific =>
							specific,

						_ =>
							Get(typeof(Exception)) switch
							{
								{ } generic =>
									generic,

								_ =>
									fallback
							}
					}
			};

			// Handle the exception
			handle(result, ex);

			// Fallback handler
			void fallback(TResult result, Exception ex) =>
				result.AddMsg(exceptionMsg(ex));
		}

		/// <summary>
		/// Clear all handlers
		/// </summary>
		public void Dispose() =>
			handlers.Clear();
	}
}
