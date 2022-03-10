// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.CustomFields.CustomField_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange
		var key = Rnd.Str;
		var value = Rnd.Int;

		// Act
		var result = Substitute.ForPartsOf<CustomField<int>>(key, value);

		// Assert
		Assert.Equal(key, result.Key);
		Assert.Equal(value, result.ValueObj);
	}
}
