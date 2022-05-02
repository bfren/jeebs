// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Data.Query;
using Jeebs.Data.Testing.Exceptions;
using Jeebs.Functions;
using Jeebs.Reflection;
using NSubstitute.Core;
using StrongId;
using Xunit.Sdk;

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
	/// <exception cref="MethodNameException"></exception>
	internal static void AssertMethodName(ICall call, string expected)
	{
		var actual = call.GetMethodInfo().Name;

		try
		{
			Assert.Equal(expected, actual);
		}
		catch (EqualException ex)
		{
			throw new MethodNameException($"Expected call to '{expected}' but received call to '{actual}'.", ex);
		}
	}

	/// <summary>
	/// Assert that <paramref name="call"/> has one generic argument, of type <typeparamref name="TExpected"/>
	/// </summary>
	/// <typeparam name="TExpected">Generic argument</typeparam>
	/// <param name="call">Call</param>
	/// <exception cref="GenericArgumentException"></exception>
	internal static void AssertGenericArgument<TExpected>(ICall call)
	{
		// Check correct number of generic arguments
		var args = call.GetMethodInfo().GetGenericArguments();
		if (args.Length == 0)
		{
			throw new GenericArgumentException("Expected one generic argument but found none.");
		}
		if (args.Length > 1)
		{
			throw new GenericArgumentException($"Expected one generic argument but found {args.Length}.");
		}

		// Check generic argument
		var expected = typeof(TExpected);
		var actual = args[0];
		try
		{
			Assert.Equal(expected, actual);
		}
		catch (EqualException ex)
		{
			throw new GenericArgumentException($"Expected type '{expected}' but found '{actual}'.", ex);
		}
	}

	/// <summary>
	/// Assert that <paramref name="actual"/> is of type <typeparamref name="T"/> and equal to
	/// <paramref name="expected"/>
	/// </summary>
	/// <typeparam name="T">Value Type</typeparam>
	/// <param name="expected">Expected value</param>
	/// <param name="actual">Actual value</param>
	/// <exception cref="EqualTypeException"></exception>
	internal static void AssertEqualType<T>(T expected, object? actual)
	{
		try
		{
			Assert.Equal(
				expected: expected,
				actual: Assert.IsType<T>(actual)
			);
		}
		catch (IsTypeException ex)
		{
			throw new EqualTypeException($"Expected type '{typeof(T)}' but value was type '{actual?.GetType()}'.", ex);
		}
		catch (EqualException ex)
		{
			throw new EqualTypeException($"Expected '{expected}' but value was '{actual}'.", ex);
		}
	}

	/// <summary>
	/// Assert that <paramref name="actual"/> and <paramref name="expected"/> are equal by
	/// serialising as JSON - to support anonymous types
	/// </summary>
	/// <param name="expected">Expected value</param>
	/// <param name="actual">Actual value</param>
	/// <exception cref="EqualJsonException"></exception>
	internal static void AssertEqualJson(object expected, object? actual)
	{
		try
		{
			Assert.Equal(
				expected: JsonF.Serialise(expected).UnsafeUnwrap(),
				actual: JsonF.Serialise(actual).UnsafeUnwrap()
			);
		}
		catch (EqualException ex)
		{
			throw new EqualJsonException($"Expected '{expected}' but value was '{actual}'.", ex);
		}
	}

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

	/// <summary>Used in <see cref="IFake"/></summary>
	private sealed record class FakeId : LongId;

	/// <summary>Used in <see cref="IFake"/></summary>
	/// <param name="Id">ID</param>
	private sealed record class FakeEntity(FakeId Id) : IWithId<FakeId>;

	/// <summary>Used to get strongly-typed method names</summary>
	private interface IFake : IFluentQuery<FakeEntity, FakeId> { }
}
