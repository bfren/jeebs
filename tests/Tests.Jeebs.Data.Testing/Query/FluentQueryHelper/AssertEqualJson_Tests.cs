// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Testing.Exceptions;

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
	public void Different_Property_Names__Equal_Values__Throws_EqualJsonException()
	{
		// Arrange
		var value = Rnd.Str;
		var v0 = new { v0 = value };
		var v1 = new { v1 = value };

		// Act
		var result = Record.Exception(() => FluentQueryHelper.AssertEqualJson(v0, v1));

		// Assert
		var ex = Assert.IsType<EqualJsonException>(result);
		Assert.Equal($"Expected '{v0}' but value was '{v1}'.", ex.Message);
	}

	[Fact]
	public void Same_Property_Names__Different_Values__Throws_EqualJsonException()
	{
		// Arrange
		var v0 = new { v0 = Rnd.Str };
		var v1 = new { v1 = Rnd.Str };

		// Act
		var result = Record.Exception(() => FluentQueryHelper.AssertEqualJson(v0, v1));

		// Assert
		var ex = Assert.IsType<EqualJsonException>(result);
		Assert.Equal($"Expected '{v0}' but value was '{v1}'.", ex.Message);
	}
}
