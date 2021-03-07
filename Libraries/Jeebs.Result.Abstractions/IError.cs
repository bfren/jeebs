// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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