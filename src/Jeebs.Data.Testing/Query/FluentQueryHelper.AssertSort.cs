// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Query;
using Jeebs.Reflection;
using NSubstitute.Core;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <inheritdoc cref="AssertSort{TEntity, TValue}(ICall, string, SortOrder)"/>
	public static void AssertSort<TEntity, TValue>(
		ICall call,
		Expression<Func<TEntity, TValue>> expectedProperty,
		SortOrder expectedOrder
	) =>
		AssertSort<TEntity, TValue>(call, expectedProperty.GetPropertyInfo().UnsafeUnwrap().Name, expectedOrder);

	/// <summary>
	/// Validate a call to <see cref="IFluentQuery{TEntity, TId}.Sort{TValue}(Expression{Func{TEntity, TValue}}, SortOrder)"/>
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TValue">Column select value type</typeparam>
	/// <param name="call">Call</param>
	/// <param name="expectedProperty">Expected property</param>
	/// <param name="expectedOrder">Expected sort order</param>
	public static void AssertSort<TEntity, TValue>(ICall call, string expectedProperty, SortOrder expectedOrder)
	{
		// Check the method
		AssertMethodName(call, nameof(IFake.Sort));

		// Check the generic arguments
		AssertGenericArgument<TValue>(call);

		// Check each predicate
		Assert.Collection(call.GetArguments(),

			// Check that the correct property is being used
			actualProperty => AssertPropertyExpression<TEntity, TValue>(expectedProperty, actualProperty),

			// Check that the correct order is being used
			actualCompare => AssertEqualType(expectedOrder, actualCompare)
		);
	}
}
