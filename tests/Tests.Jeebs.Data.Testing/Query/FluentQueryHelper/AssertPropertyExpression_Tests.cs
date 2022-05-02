// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Xunit.Sdk;

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
	public void Not_Expression__Throws_IsAssignableFromException()
	{
		// Arrange

		// Act
		var action = () => FluentQueryHelper.AssertPropertyExpression<Test, string>(Rnd.Str, Rnd.Str);

		// Assert
		Assert.Throws<IsAssignableFromException>(action);
	}

	[Fact]
	public void Not_Equal__Throws_EqualException()
	{
		// Arrange
		var expected = nameof(Test.Bar);
		Expression<Func<Test, string>> actual = x => x.Foo;

		// Act
		var action = () => FluentQueryHelper.AssertPropertyExpression<Test, string>(expected, actual);

		// Assert
		Assert.Throws<EqualException>(action);
	}

	public sealed record class Test(string Foo, int Bar);
}
