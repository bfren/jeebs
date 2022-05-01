// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Data.Query;
using Jeebs.Functions;
using Jeebs.Reflection;
using NSubstitute.Core;
using StrongId;

namespace Jeebs.Data.Testing.Query;

/// <summary>
/// <see cref="IFluentQuery{TEntity, TId}"/> unit test helper functions and assertions
/// </summary>
public static partial class FluentQueryHelper
{
	/// <summary>
	/// Assert that <paramref name="call"/> is a call to a method called <paramref name="expected"/>
	/// </summary>
	/// <param name="call">Call</param>
	/// <param name="expected">Expected method name</param>
	private static void AssertMethodName(ICall call, string expected) =>
		Assert.Equal(
			expected: expected,
			actual: call.GetMethodInfo().Name
		);

	/// <summary>
	/// Assert that <paramref name="call"/> has one generic argument, of type <typeparamref name="TExpected"/>
	/// </summary>
	/// <typeparam name="TExpected">Generic argument</typeparam>
	/// <param name="call">Call</param>
	private static void AssertGenericArgument<TExpected>(ICall call) =>
		Assert.Collection(call.GetMethodInfo().GetGenericArguments(),
			actual => Assert.Equal(
				expected: typeof(TExpected),
				actual: actual
			)
		);

	/// <summary>
	/// Assert that <paramref name="actual"/> is of type <typeparamref name="T"/> and equal to
	/// <paramref name="expected"/>
	/// </summary>
	/// <typeparam name="T">Value Type</typeparam>
	/// <param name="expected">Expected value</param>
	/// <param name="actual">Actual value</param>
	private static void AssertEqual<T>(T expected, object? actual) =>
		Assert.Equal(
			expected: expected,
			actual: Assert.IsType<T>(actual)
		);

	/// <summary>
	/// Assert that <paramref name="actual"/> and <paramref name="expected"/> are equal by
	/// serialising as JSON - to support anonymous types
	/// </summary>
	/// <param name="expected">Expected value</param>
	/// <param name="actual">Actual value</param>
	private static void AssertEqualJson(object expected, object? actual) =>
		Assert.Equal(
			expected: JsonF.Serialise(expected),
			actual: JsonF.Serialise(actual)
		);

	/// <summary>
	/// Assert that <paramref name="actual"/> is an <see cref="Expression{TDelegate}"/> that resolves
	/// to a property with name <paramref name="expected"/> <see cref="Func{TEntity, TValue}"/>
	/// </summary>
	/// <typeparam name="TEntity">Entity Type</typeparam>
	/// <typeparam name="TValue">Value Type</typeparam>
	/// <param name="expected">Expected property name</param>
	/// <param name="actual">Actual property (will be cast to <see cref="Expression{TDelegate}"/>)</param>
	private static void AssertPropertyExpression<TEntity, TValue>(string expected, object? actual) =>
		Assert.Equal(
			expected: expected,
			actual: Assert.IsAssignableFrom<Expression<Func<TEntity, TValue>>>(actual)
				.GetPropertyInfo().UnsafeUnwrap().Name
		);

	/// <summary>Used in <see cref="IFake"/></summary>
	private sealed record class FakeId : LongId;

	/// <summary>Used in <see cref="IFake"/></summary>
	/// <param name="Id">ID</param>
	private sealed record class FakeEntity(FakeId Id) : IWithId<FakeId>;

	/// <summary>Used to get strongly-typed method names</summary>
	private interface IFake : IFluentQuery<FakeEntity, FakeId> { }
}
