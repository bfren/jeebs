using System;

namespace Jeebs
{
	/// <summary>
	/// Result interface
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
	}
}
