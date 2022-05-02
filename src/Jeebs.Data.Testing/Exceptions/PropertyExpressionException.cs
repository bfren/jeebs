// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Testing.Exceptions;

/// <summary>
/// See <see cref="Query.FluentQueryHelper.AssertPropertyExpression{TEntity, TValue}(string, object?)"/>
/// </summary>
public sealed class PropertyExpressionException : FluentQueryHelperException
{
	/// <inheritdoc/>
	public PropertyExpressionException() { }

	/// <inheritdoc/>
	public PropertyExpressionException(string message) : base(message) { }

	/// <inheritdoc/>
	public PropertyExpressionException(string message, Exception inner) : base(message, inner) { }
}
