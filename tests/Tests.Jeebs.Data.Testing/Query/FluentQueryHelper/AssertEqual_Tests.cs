// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertEqual_Tests
{
	[Fact]
	public void Asserts_Equal()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var action = () => FluentQueryHelper.AssertEqual(value, value);

		// Assert
		action();
	}

	[Fact]
	public void Incorrect_Type__Throws_IsTypeException()
	{
		// Arrange
		var value = Rnd.Lng;

		// Act
		var action = () => FluentQueryHelper.AssertEqual(value, value.ToString());

		// Assert
		Assert.Throws<IsTypeException>(action);
	}

	[Fact]
	public void Not_Equal__Throws_EqualException()
	{
		// Arrange

		// Act
		var action = () => FluentQueryHelper.AssertEqual(Rnd.Str, Rnd.Str);

		// Assert
		Assert.Throws<EqualException>(action);
	}
}
