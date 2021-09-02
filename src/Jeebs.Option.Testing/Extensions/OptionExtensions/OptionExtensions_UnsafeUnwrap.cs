// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

/// <summary>
/// Option Extensions: UnsafeUnwrap
/// </summary>
public static class OptionExtensions_UnsafeUnwrap
{
	/// <summary>
	/// Assume <paramref name="this"/> is a <see cref="Some{T}"/> and get the value -
	/// useful to get values during the Arrange section of a unit test
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	/// <param name="this">Option</param>
	public static T UnsafeUnwrap<T>(this Option<T> @this) =>
		@this.Unwrap(() => throw new UnsafeUnwrapException());
}

/// <summary>
/// Thrown when <see cref="OptionExtensions_UnsafeUnwrap.UnsafeUnwrap{T}(Option{T})"/> fails
/// </summary>
public class UnsafeUnwrapException : System.Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public UnsafeUnwrapException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public UnsafeUnwrapException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnsafeUnwrapException(string message, System.Exception inner) : base(message, inner) { }
}
