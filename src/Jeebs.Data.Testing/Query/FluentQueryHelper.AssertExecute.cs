// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Data.Query;
using Jeebs.Reflection;
using NSubstitute.Core;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <inheritdoc cref="AssertExecute{TEntity, TValue}(ICall, string, bool)"/>
	public static void AssertExecute<TEntity, TValue>(ICall call, Expression<Func<TEntity, TValue>> expected, bool withTransaction) =>
		AssertExecute<TEntity, TValue>(call, expected.GetPropertyInfo().UnsafeUnwrap().Name, withTransaction);

	/// <summary>
	/// Validate a call to <see cref="IFluentQuery{TEntity, TId}.ExecuteAsync{TValue}(Expression{Func{TEntity, TValue}})"/>
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TValue">Column select value type</typeparam>
	/// <param name="call">Call</param>
	/// <param name="expected">Expected property</param>
	/// <param name="withTransaction">Whether or not to check for a transaction</param>
	public static void AssertExecute<TEntity, TValue>(ICall call, string expected, bool withTransaction)
	{
		// Check the method name
		AssertMethodName(call, nameof(IFake.ExecuteAsync));

		// Check the generic arguments
		AssertGenericArgument<TValue>(call);

		// Check the parameters
		var inspectors = new List<Action<object?>>
		{
			// Check that the correct property is being used
			{ actualProperty => AssertPropertyExpression<TEntity, TValue>(expected, actualProperty) }
		};

		if (withTransaction)
		{
			// Use discard as a placeholder so the right number of items is checked in the collection
			inspectors.Add(_ => { });
		}

		AssertCollection(call.GetArguments(), inspectors.ToArray());
	}
}
