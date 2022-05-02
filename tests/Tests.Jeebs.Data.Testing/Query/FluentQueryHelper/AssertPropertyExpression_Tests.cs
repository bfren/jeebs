// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs.Data.Testing.Exceptions;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertPropertyExpression_Tests
{
	[Fact]
	public void Asserts_Equal()
	{
		// Arrange
		var expected = nameof(Test.Foo);
		Expression<Func<Test, string>> actual = x => x.Foo;

		// Act
		var action = () => FluentQueryHelper.AssertPropertyExpression<Test, string>(expected, actual);

		// Assert
		action();
	}

	[Fact]
	public void Not_Expression__Throws_PropertyExpressionException()
	{
		// Arrange

		// Act
		var action = () => FluentQueryHelper.AssertPropertyExpression<Test, string>(Rnd.Str, Rnd.Str);

		// Assert
		var ex = Assert.Throws<PropertyExpressionException>(action);
		Assert.Equal($"Expected a property expression but received '{typeof(string)}'.", ex.Message);
	}

	[Fact]
	public void Not_Equal__Throws_PropertyExpressionException()
	{
		// Arrange
		var expected = nameof(Test.Bar);
		Expression<Func<Test, string>> actual = x => x.Foo;

		// Act
		var action = () => FluentQueryHelper.AssertPropertyExpression<Test, string>(expected, actual);

		// Assert
		var ex = Assert.Throws<PropertyExpressionException>(action);
		Assert.Equal($"Expected property with name '{expected}' but received '{nameof(Test.Foo)}'.", ex.Message);
	}

	public sealed record class Test(string Foo, int Bar);
}
