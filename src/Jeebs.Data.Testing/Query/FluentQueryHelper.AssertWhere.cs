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
	/// <inheritdoc cref="AssertWhere{TEntity, TValue}(ICall, string, Compare, TValue)"/>
	public static void AssertWhere<TEntity, TValue>(
		ICall call,
		Expression<Func<TEntity, TValue>> expectedProperty,
		Compare expectedCompare,
		TValue expectedValue
	) =>
		AssertWhere<TEntity, TValue>(
			call,
			expectedProperty.GetPropertyInfo().UnsafeUnwrap().Name,
			expectedCompare,
			expectedValue
		);

	/// <summary>
	/// Validate a call to <see cref="IFluentQuery{TEntity, TId}.Where"/>
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TValue">Column select value type</typeparam>
	/// <param name="call">Call</param>
	/// <param name="expectedProperty">Expected property</param>
	/// <param name="expectedCompare">Expected comparison</param>
	/// <param name="expectedValue">Expected value</param>
	public static void AssertWhere<TEntity, TValue>(
		ICall call,
		string expectedProperty,
		Compare expectedCompare,
		TValue expectedValue
	)
	{
		// Check the method name
		AssertMethodName(call, nameof(IFake.Where));

		// Check the generic arguments
		AssertGenericArgument<TValue>(call);

		// Check the parameters
		Assert.Collection(call.GetArguments(),

			// Check that the correct property is being used
			actualProperty => AssertPropertyExpression<TEntity, TValue>(expectedProperty, actualProperty),

			// Check that the correct compare is being used
			actualCompare => AssertEqual(expectedCompare, actualCompare),

			// Check that the correct value is being used
			actualValue =>
			{
				if (actualValue is null)
				{
					Assert.Null(actualValue);
				}
				else
				{
					Assert.Equal(expectedValue!, actualValue);
				}
			}
		);
	}

	/// <summary>
	/// Validate a call to <see cref="IFluentQuery{TEntity, TId}.Where(string, object)"/>
	/// </summary>
	/// <param name="call">Call</param>
	/// <param name="expectedClause">Expected clause</param>
	/// <param name="expectedParameters">Expected parameters</param>
	public static void AssertWhere(ICall call, string expectedClause, object expectedParameters)
	{
		// Check the method name
		AssertMethodName(call, nameof(IFake.Where));

		// Check the parameters
		Assert.Collection(call.GetArguments(),

			// Check that the correct clause is being used
			actualClause => AssertEqual(expectedClause, actualClause),

			// Check that the correct parameters are being used -
			// using Json serialisation to support anonymous types
			actualParameters => AssertEqualJson(expectedParameters, actualParameters)
		);
	}
}
