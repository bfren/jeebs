using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for IR interface: Link
	/// </summary>
	public static class RExtensions_Link
	{
		/// <summary>
		/// Create a new link in the result chain
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result</param>
		public static ILink<TValue> Link<TValue>(this IR<TValue> @this)
			=> new Link<TValue>(@this);

		/// <summary>
		/// Create a new link in the result chain
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Result state type</typeparam>
		/// <param name="this">Result</param>
		public static ILink<TValue, TState> Link<TValue, TState>(this IR<TValue, TState> @this)
			=> new Link<TValue, TState>(@this);

		/// <summary>
		/// Await a link in the result chain
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="this">Result</param>
		public static TResult Await<TResult>(this Task<TResult> @this)
			where TResult : IR
			=> @this.GetAwaiter().GetResult();
	}
}
