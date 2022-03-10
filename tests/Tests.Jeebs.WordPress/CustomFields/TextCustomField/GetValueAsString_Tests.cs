// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.CustomFields.TextCustomField_Tests;

public class GetValueAsString_Tests
{
	[Fact]
	public void Returns_ValueObj()
	{
		// Arrange
		var value = Rnd.Str;
		var field = new Test(Rnd.Str, value);

		// Act
		var result = field.GetValueAsStringTest();

		// Assert
		Assert.Equal(value, result);
	}

	public class Test : TextCustomField
	{
		public Test(string key, string value) : base(key) =>
			ValueObj = value;
	}
}
