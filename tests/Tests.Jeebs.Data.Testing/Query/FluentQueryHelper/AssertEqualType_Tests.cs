// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Testing.Exceptions;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertEqualType_Tests
{
	[Fact]
	public void Asserts_Equal()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var action = () => FluentQueryHelper.AssertEqualType(value, value);

		// Assert
		action();
	}

	[Fact]
	public void Incorrect_Type__Throws_EqualTypeException()
	{
		// Arrange
		var value = Rnd.Lng;

		// Act
		var action = () => FluentQueryHelper.AssertEqualType(value, value.ToString());

		// Assert
		var ex = Assert.Throws<EqualTypeException>(action);
		Assert.Equal($"Expected type '{typeof(long)}' but value was type '{typeof(string)}'.", ex.Message);
	}

	[Fact]
	public void Not_Equal__Throws_EqualTypeException()
	{
		// Arrange
		var v0 = Rnd.Str;
		var v1 = Rnd.Str;

		// Act
		var action = () => FluentQueryHelper.AssertEqualType(v0, v1);

		// Assert
		var ex = Assert.Throws<EqualTypeException>(action);
		Assert.Equal($"Expected '{v0}' but value was '{v1}'.", ex.Message);
	}
}
