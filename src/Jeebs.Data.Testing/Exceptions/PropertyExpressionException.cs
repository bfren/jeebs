// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Testing.Exceptions;

/// <summary>
/// See <see cref="Query.FluentQueryHelper.AssertPropertyExpression{TEntity, TValue}(string, object?)"/>
/// </summary>
public sealed class PropertyExpressionException : Exception
{
	/// <summary>
	/// Create
	/// </summary>
	public PropertyExpressionException() { }

	/// <summary>
	/// Create with message
	/// </summary>
	/// <param name="message"></param>
	public PropertyExpressionException(string message) : base(message) { }

	/// <summary>
	/// Create with message and inner exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public PropertyExpressionException(string message, Exception inner) : base(message, inner) { }
}
