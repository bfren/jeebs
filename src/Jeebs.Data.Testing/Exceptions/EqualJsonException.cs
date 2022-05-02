// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Testing.Exceptions;

/// <summary>
/// See <see cref="Query.FluentQueryHelper.AssertEqualJson(object, object?)"/>
/// </summary>
public sealed class EqualJsonException : FluentQueryHelperException
{
	/// <inheritdoc/>
	public EqualJsonException() { }

	/// <inheritdoc/>
	public EqualJsonException(string message) : base(message) { }

	/// <inheritdoc/>
	public EqualJsonException(string message, Exception inner) : base(message, inner) { }
}
