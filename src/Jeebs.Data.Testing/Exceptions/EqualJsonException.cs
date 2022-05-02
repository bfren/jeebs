// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Testing.Exceptions;

/// <summary>
/// See <see cref="Query.FluentQueryHelper.AssertEqualJson(object, object?)"/>
/// </summary>
public sealed class EqualJsonException : Exception
{
	/// <summary>
	/// Create
	/// </summary>
	public EqualJsonException() { }

	/// <summary>
	/// Create with message
	/// </summary>
	/// <param name="message"></param>
	public EqualJsonException(string message) : base(message) { }

	/// <summary>
	/// Create with message and inner exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public EqualJsonException(string message, Exception inner) : base(message, inner) { }
}
