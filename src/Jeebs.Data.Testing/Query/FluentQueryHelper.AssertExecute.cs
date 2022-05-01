// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Data.Query;
using Jeebs.Reflection;
using NSubstitute.Core;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <inheritdoc cref="AssertExecute{TEntity, TValue}(ICall, string)"/>
	public static void AssertExecute<TEntity, TValue>(ICall call, Expression<Func<TEntity, TValue>> expected) =>
		AssertExecute<TEntity, TValue>(call, expected.GetPropertyInfo().UnsafeUnwrap().Name);

	/// <summary>
	/// Validate a call to <see cref="IFluentQuery{TEntity, TId}.ExecuteAsync"/>
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TValue">Column select value type</typeparam>
	/// <param name="call">Call</param>
	/// <param name="expected">Expected property</param>
	public static void AssertExecute<TEntity, TValue>(ICall call, string expected)
	{
		// Check the method name
		AssertMethodName(call, nameof(IFake.ExecuteAsync));

		// Check the generic arguments
		AssertGenericArgument<TValue>(call);

		// Check the parameters
		Assert.Collection(call.GetArguments(),

			// Check that the correct property is being used
			actualProperty => AssertPropertyExpression<TEntity, TValue>(expected, actualProperty)
		);
	}
}
