// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Exceptions;

/// <summary>
/// Thrown when an unknown <see cref="Option{T}"/> type is matched -
/// as <see cref="Option{T}"/> only allows internal implementation this should never happen...
/// </summary>
public class UnknownOptionException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public UnknownOptionException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public UnknownOptionException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnknownOptionException(string message, Exception inner) : base(message, inner) { }
}
