// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.CustomFields.CustomField_Tests;

public class ToString_Tests
{
	[Fact]
	public void ValueObj_Not_Null_Returns_ValueObj_ToString()
	{
		// Arrange
		var key = Rnd.Str;
		var value = Rnd.Guid;
		var field = Substitute.ForPartsOf<CustomField<Guid>>(key, value);

		// Act
		var result = field.ToString();

		// Assert
		Assert.Equal(value.ToString(), result);
	}

	[Fact]
	public void ValueObj_Null_Returns_ValueStr()
	{
		// Arrange
		var key = Rnd.Str;
		Guid? value = null;
		string str = Rnd.Str;
		var field = Substitute.ForPartsOf<Test>(key, value, str);

		// Act
		var result = field.ToString();

		// Assert
		Assert.Equal(str, result);
	}

	[Fact]
	public void ValueObj_And_ValueStr_Null_Returns_Key()
	{
		// Arrange
		var key = Rnd.Str;
		Guid? value = null;
		var field = Substitute.ForPartsOf<CustomField<Guid?>>(key, value);

		// Act
		var result = field.ToString();

		// Assert
		Assert.Equal(key, result);
	}

	public abstract class Test : CustomField<Guid?>
	{
		protected Test(string key, Guid? value, string str) : base(key, value) =>
			ValueStr = str;
	}
}
