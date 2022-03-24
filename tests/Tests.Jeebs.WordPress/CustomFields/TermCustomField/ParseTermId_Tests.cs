// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.WordPress.CustomFields.TermCustomField.M;

namespace Jeebs.WordPress.CustomFields.TermCustomField_Tests;

public class ParseTermId_Tests
{
	[Fact]
	public void Invalid_Long_Returns_None_With_ValueIsInvalidTermIdMsg()
	{
		// Arrange
		var type = typeof(ParseTermId_Tests);
		var value = Rnd.Str;

		// Act
		var result = TermCustomField.ParseTermId(type, value);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<ValueIsInvalidTermIdMsg>(none);
	}

	[Fact]
	public void Valid_Long_Returns_TermId()
	{
		// Arrange
		var type = typeof(ParseTermId_Tests);
		var value = Rnd.Lng;

		// Act
		var result = TermCustomField.ParseTermId(type, value.ToString());

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some.Value);
	}
}
