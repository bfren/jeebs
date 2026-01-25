// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

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
		_ = result.AssertFail("'{Value}' is not a valid Term ID.", value);
	}

	[Fact]
	public void Valid_Long_Returns_TermId()
	{
		// Arrange
		var type = typeof(ParseTermId_Tests);
		var value = Rnd.ULng;

		// Act
		var result = TermCustomField.ParseTermId(type, value.ToString());

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(value, ok.Value);
	}
}
