using System;

namespace Jeebs
{
	/// <summary>
	/// Enables passing of results, with value and errors
	/// </summary>
	/// <typeparam name="T">Result success value type</typeparam>
	public interface IResult<T>
	{
		/// <summary>
		/// Success value - you MUST use <code>Result.Err is IErrorList</code> (or similar) before accessing this
		/// </summary>
		T Val { get; }

		/// <summary>
		/// List of errors when the result is a failure
		/// Use <code>Result.Err is IErrorList</code> to check before accessing Result.Val
		/// </summary>
		IErrorList? Err { get; }

		/// <summary>
		/// Pipe the current result into a new function on success - bubbles up errors if not
		/// </summary>
		/// <typeparam name="TNew">New value type</typeparam>
		/// <param name="next">Next function to perform</param>
		IResult<TNew> Pipe<TNew>(Func<T, IResult<TNew>> next);
	}
}
