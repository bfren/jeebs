// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.CustomFields.TextCustomField_Tests;

public class ValueObj_Tests
{
	[Fact]
	public void Returns_ValueStr_If_Not_Null()
	{
		// Arrange
		var value = Rnd.Str;
		var field = new Test(Rnd.Str, value);

		// Act
		var result = field.ValueObj;

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void Returns_Empty_String_If_ValueStr_Is_Null()
	{
		// Arrange
		var field = new Test(Rnd.Str, null);

		// Act
		var result = field.ValueObj;

		// Assert
		Assert.Equal(string.Empty, result);
	}

	public class Test : TextCustomField
	{
		public Test(string key, string? value) : base(key) =>
			ValueStr = value;
	}
}
