// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Data.Query;
using Jeebs.Reflection;
using NSubstitute.Core;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <inheritdoc cref="AssertWhereNotIn{TEntity, TValue}(ICall, string, TValue[])"/>
	public static void AssertWhereNotIn<TEntity, TValue>(
		ICall call,
		Expression<Func<TEntity, TValue>> expectedProperty,
		TValue[] expectedValues
	) =>
		AssertWhereNotIn<TEntity, TValue>(
			call,
			expectedProperty.GetPropertyInfo().UnsafeUnwrap().Name,
			expectedValues
		);

	/// <summary>
	/// Validate a call to <see cref="IFluentQuery{TEntity, TId}.WhereNotIn"/>
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TValue">Column select value type</typeparam>
	/// <param name="call">Call</param>
	/// <param name="expectedProperty">Expected property</param>
	/// <param name="expectedValues">Expected values</param>
	public static void AssertWhereNotIn<TEntity, TValue>(ICall call, string expectedProperty, TValue[] expectedValues)
	{
		// Check the method
		AssertMethodName(call, nameof(IFake.WhereNotIn));

		// Check the generic arguments
		AssertGenericArgument<TValue>(call);

		// Check each parameters
		Assert.Collection(call.GetArguments(),

			// Check that the correct property is being used
			actualProperty => AssertPropertyExpression<TEntity, TValue>(expectedProperty, actualProperty),

			// Check that the correct values are being used
			actualValue => AssertEqualType(expectedValues, actualValue)
		);
	}
}
