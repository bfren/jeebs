// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="IR"/>: Await
	/// </summary>
	public static class RExtensions_Await
	{
		/// <summary>
		/// Await a link in the result chain
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="this">Result</param>
		public static TResult Await<TResult>(this Task<TResult> @this)
			where TResult : IR =>
			@this.GetAwaiter().GetResult();
	}
}
