// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

/// <summary>
/// Maybe Extensions: UnsafeUnwrap
/// </summary>
public static class OptionExtensionsUnsafeUnwrap
{
	/// <summary>
	/// Assume <paramref name="this"/> is a <see cref="Internals.Some{T}"/> and get the value -
	/// useful to get values during the Arrange section of a unit test
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="this">Maybe</param>
	public static T UnsafeUnwrap<T>(this Maybe<T> @this) =>
		@this.Unwrap(() => throw new UnsafeUnwrapException());
}

/// <summary>
/// Thrown when <see cref="OptionExtensionsUnsafeUnwrap.UnsafeUnwrap{T}(Maybe{T})"/> fails
/// </summary>
public class UnsafeUnwrapException : System.Exception
{
	/// <summary>Create exception</summary>
	public UnsafeUnwrapException() { }

	/// <summary>Create exception</summary>
	/// <param name="message">Message</param>
	public UnsafeUnwrapException(string message) : base(message) { }

	/// <summary>Create exception</summary>
	/// <param name="message">Message</param>
	/// <param name="inner">Inner Exception</param>
	public UnsafeUnwrapException(string message, System.Exception inner) : base(message, inner) { }
}
