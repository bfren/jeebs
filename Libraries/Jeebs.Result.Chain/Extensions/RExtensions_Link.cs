using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public static class RExtensions_Link
	{
		public static ILink Link(this IR @this)
			=> new Link(@this);

		public static ILink<TValue> Link<TValue>(this IR<TValue> @this)
			=> new Link<TValue>(@this);

		public static ILink<TValue, TState> Link<TValue, TState>(this IR<TValue, TState> @this)
			=> new Link<TValue, TState>(@this);

		public static TResult Await<TResult>(this Task<TResult> @this)
			where TResult : IR
			=> @this.GetAwaiter().GetResult();
	}
}
