// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Testing.Exceptions;

/// <summary>
/// Fluent Query Exception type.
/// </summary>
public abstract class FluentQueryHelperException : Exception
{
	/// <summary>
	/// Create blank exception.
	/// </summary>
	protected FluentQueryHelperException() { }

	/// <summary>
	/// Create exception with message.
	/// </summary>
	/// <param name="message"></param>
	protected FluentQueryHelperException(string message) : base(message) { }

	/// <summary>
	/// Create exception with message and inner exception.
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	protected FluentQueryHelperException(string message, Exception inner) : base(message, inner) { }
}
