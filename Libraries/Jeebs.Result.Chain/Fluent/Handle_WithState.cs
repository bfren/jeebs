using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Result.Chain.Fluent
{
	public sealed class Handle<TValue, TState, TException>
		where TException : Exception
	{
		private readonly ILink<TValue, TState> link;

		internal Handle(ILink<TValue, TState> link)
			=> this.link = link;

		public ILink<TValue, TState> With(Action<IR<TValue, TState>, TException> handler)
		{
			link.AddExceptionHandler(handler);
			return link;
		}

		public ILink<TValue, TState> With(Func<IR<TValue, TState>, TException, Task> asyncHandler)
		{
			link.AddExceptionHandler<TException>(async (r, ex) => await asyncHandler(r, ex).ConfigureAwait(false));
			return link;
		}

		public ILink<TValue, TState> With<TExceptionMsg>()
			where TExceptionMsg : IExceptionMsg, new()
			=> With((r, ex) =>
			{
				var msg = new TExceptionMsg();
				msg.Set(ex);
				r.AddMsg(msg);
			});
	}
}
