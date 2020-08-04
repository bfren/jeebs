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
		/// Create a new link in the result chain - special case for starting links when all we have is an <see cref="IOk"/>
		/// </summary>
		/// <param name="this">Result</param>
		public static ILink<bool> Link(this IR @this)
			=> new Link<bool>(@this);

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
	}
}
