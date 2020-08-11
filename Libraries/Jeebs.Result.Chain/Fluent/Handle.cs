using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Result.Chain.Fluent
{
	public sealed class Handle<TValue, TException>
		where TException : Exception
	{
		private readonly ILink<TValue> link;

		internal Handle(ILink<TValue> link)
			=> this.link = link;

		public ILink<TValue> With(Action<IR<TValue>, TException> handler)
		{
			link.AddExceptionHandler(handler);
			return link;
		}

		public ILink<TValue> With(Func<IR<TValue>, TException, Task> asyncHandler)
		{
			link.AddExceptionHandler<TException>(async (r, ex) => await asyncHandler(r, ex).ConfigureAwait(false));
			return link;
		}

		public ILink<TValue> With<TExceptionMsg>()
			where TExceptionMsg : IExceptionMsg, new()
			=> With((r, ex) =>
			{
				var msg = new TExceptionMsg();
				msg.Set(ex);
				r.AddMsg(msg);
			});
	}
}
