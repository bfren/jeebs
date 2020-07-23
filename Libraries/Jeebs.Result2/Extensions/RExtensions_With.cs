using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Result.Fluent;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for IR interface: With
	/// </summary>
	public static class RExtensions_With
	{
		/// <summary>
		/// Begin a fluent With operation, used to add message(s) to the result chain
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="result">Result</param>
		public static With<TResult> With<TResult>(this TResult result)
			where TResult : IR => new With<TResult>(result);
	}
}
