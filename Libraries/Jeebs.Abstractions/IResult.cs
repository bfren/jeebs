using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Simple structure for safely performing operations and returning a successful result, or a list of errors
	/// </summary>
	public interface IResult<TSuccess>
	{
		/// <summary>
		/// Message to return with the result
		/// </summary>
		string Message { get; }
	}

	/// <summary>
	/// Simple structure for safely performing operations and returning a successful result, or a list of errors
	/// Special case - to be used instead of a return type of 'bool' for success / failure
	/// </summary>
	public interface IResult : IResult<bool> { }
}
