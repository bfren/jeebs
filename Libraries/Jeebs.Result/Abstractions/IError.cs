using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// General result error, used for pattern matching, for example:
	/// <c>r is IError e</c>
	/// </summary>
	public interface IError : IR { }

	/// <summary>
	/// Main result error
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	public interface IError<TValue> : IError, IR<TValue> { }
}