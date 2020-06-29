using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents an error result.
	/// </summary>
	/// <typeparam name="TResult">Result value type - not used in Error classes, only Ok classes have a value</typeparam>
	public interface IError<TResult> : IR<TResult> { }

	/// <summary>
	/// Represents a simple error result.
	/// </summary>
	public interface IError : IError<bool> { }
}
