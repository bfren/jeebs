using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jm;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="IR"/>: Link
	/// </summary>
	public static class RExtensions_Link
	{
		/// <summary>
		/// Create a new link in the result chain - special case for starting links when all we have is an <see cref="IOk"/>
		/// </summary>
		/// <param name="this">Result</param>
		/// <param name="exceptionMsg">[Optional] Return exception msg when an exception occurs</param>
		public static ILink<bool> Link(this IR @this, Func<Exception, IMsg>? exceptionMsg = null) =>
			new Link<bool>(@this, exceptionMsg);

		/// <summary>
		/// Create a new link in the result chain
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="exceptionMsg">[Optional] Return exception msg when an exception occurs</param>
		public static ILink<TValue> Link<TValue>(this IR<TValue> @this, Func<Exception, IMsg>? exceptionMsg = null) =>
			new Link<TValue>(@this, exceptionMsg);

		/// <summary>
		/// Create a new link in the result chain
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Result state type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="exceptionMsg">[Optional] Return exception msg when an exception occurs</param>
		public static ILink<TValue, TState> Link<TValue, TState>(this IR<TValue, TState> @this, Func<Exception, IMsg>? exceptionMsg = null) =>
			new Link<TValue, TState>(@this, exceptionMsg);
	}
}
