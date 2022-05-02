// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Testing.Exceptions;

/// <summary>
/// See <see cref="Query.FluentQueryHelper.AssertMethodName(NSubstitute.Core.ICall, string)"/>
/// </summary>
public sealed class MethodNameException : Exception
{
	/// <summary>
	/// Create
	/// </summary>
	public MethodNameException() { }

	/// <summary>
	/// Create with message
	/// </summary>
	/// <param name="message"></param>
	public MethodNameException(string message) : base(message) { }

	/// <summary>
	/// Create with message and inner exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public MethodNameException(string message, Exception inner) : base(message, inner) { }
}
