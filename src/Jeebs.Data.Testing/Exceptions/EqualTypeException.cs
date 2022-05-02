// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Testing.Exceptions;

/// <summary>
/// See <see cref="Query.FluentQueryHelper.AssertEqualType{T}(T, object?)"/>
/// </summary>
public sealed class EqualTypeException : FluentQueryHelperException
{
	/// <inheritdoc/>
	public EqualTypeException() { }

	/// <inheritdoc/>
	public EqualTypeException(string message) : base(message) { }

	/// <inheritdoc/>
	public EqualTypeException(string message, Exception inner) : base(message, inner) { }
}
