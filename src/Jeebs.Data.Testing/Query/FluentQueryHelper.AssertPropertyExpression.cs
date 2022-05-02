// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Data.Testing.Exceptions;
using Jeebs.Reflection;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <summary>
	/// Assert that <paramref name="actual"/> is an <see cref="Expression{TDelegate}"/> that resolves
	/// to a property with name <paramref name="expected"/> <see cref="Func{TEntity, TValue}"/>
	/// </summary>
	/// <typeparam name="TEntity">Entity Type</typeparam>
	/// <typeparam name="TValue">Value Type</typeparam>
	/// <param name="expected">Expected property name</param>
	/// <param name="actual">Actual property (will be cast to <see cref="Expression{TDelegate}"/>)</param>
	/// <exception cref="PropertyExpressionException"></exception>
	internal static void AssertPropertyExpression<TEntity, TValue>(string expected, object? actual)
	{
		try
		{
			var actualName = Assert.IsAssignableFrom<Expression<Func<TEntity, TValue>>>(actual)
				.GetPropertyInfo().UnsafeUnwrap().Name;

			try
			{
				Assert.Equal(
					expected: expected,
					actual: actualName
				);
			}
			catch (EqualException ex)
			{
				throw new PropertyExpressionException($"Expected property with name '{expected}' but received '{actualName}'.", ex);
			}
		}
		catch (IsAssignableFromException ex)
		{
			throw new PropertyExpressionException($"Expected a property expression but received '{actual?.GetType()}'.", ex);
		}
	}
}
