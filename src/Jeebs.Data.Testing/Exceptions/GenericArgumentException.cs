// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Testing.Exceptions;

/// <summary>
/// See <see cref="Query.FluentQueryHelper.AssertGenericArgument{TExpected}(NSubstitute.Core.ICall)"/>
/// </summary>
public sealed class GenericArgumentException : FluentQueryHelperException
{
	/// <inheritdoc/>
	public GenericArgumentException() { }

	/// <inheritdoc/>
	public GenericArgumentException(string message) : base(message) { }

	/// <inheritdoc/>
	public GenericArgumentException(string message, Exception inner) : base(message, inner) { }
}
