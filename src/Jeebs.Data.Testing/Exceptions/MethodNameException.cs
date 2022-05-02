// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Testing.Exceptions;

/// <summary>
/// See <see cref="Query.FluentQueryHelper.AssertMethodName(NSubstitute.Core.ICall, string)"/>
/// </summary>
public sealed class MethodNameException : FluentQueryHelperException
{
	/// <inheritdoc/>
	public MethodNameException() { }

	/// <inheritdoc/>
	public MethodNameException(string message) : base(message) { }

	/// <inheritdoc/>
	public MethodNameException(string message, Exception inner) : base(message, inner) { }
}
