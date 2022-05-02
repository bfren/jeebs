// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Testing.Exceptions;

/// <summary>
/// See <see cref="Query.FluentQueryHelper.AssertEqualType{T}(T, object?)"/>
/// </summary>
public sealed class EqualTypeException : Exception
{
	/// <summary>
	/// Create
	/// </summary>
	public EqualTypeException() { }

	/// <summary>
	/// Create with message
	/// </summary>
	/// <param name="message"></param>
	public EqualTypeException(string message) : base(message) { }

	/// <summary>
	/// Create with message and inner exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public EqualTypeException(string message, Exception inner) : base(message, inner) { }
}
