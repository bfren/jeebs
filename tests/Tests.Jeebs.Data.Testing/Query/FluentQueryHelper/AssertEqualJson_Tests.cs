// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public class AssertEqualJson_Tests
{
	[Fact]
	public void Asserts_Equal()
	{
		// Arrange
		var value = Rnd.Str;
		var v0 = new { value };
		var v1 = new { value };

		// Act
		var action = () => FluentQueryHelper.AssertEqualJson(v0, v1);

		// Assert
		action();
	}

	[Fact]
	public void Different_Property_Names__Equal_Values__Throws_EqualException()
	{
		// Arrange
		var value = Rnd.Str;
		var v0 = new { v0 = value };
		var v1 = new { v1 = value };

		// Act
		var action = () => FluentQueryHelper.AssertEqualJson(v0, v1);

		// Assert
		Assert.Throws<EqualException>(action);
	}

	[Fact]
	public void Same_Property_Names__Different_Values__Throws_EqualException()
	{
		// Arrange
		var v0 = new { v0 = Rnd.Str };
		var v1 = new { v1 = Rnd.Str };

		// Act
		var action = () => FluentQueryHelper.AssertEqualJson(v0, v1);

		// Assert
		Assert.Throws<EqualException>(action);
	}
}
