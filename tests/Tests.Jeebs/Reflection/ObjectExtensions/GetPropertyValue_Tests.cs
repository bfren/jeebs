// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Reflection.ObjectExtensions_Tests;

public class GetPropertyValue_Tests
{
	[Fact]
	public void Property_Does_Not_Exist_Returns_None()
	{
		// Arrange
		var test = new Test(Rnd.Str);

		// Act
		var r0 = test.GetPropertyValue(Rnd.Str);
		var r1 = test.GetPropertyValue<string>(Rnd.Str);

		// Assert
		r0.AssertNone();
		r1.AssertNone();
	}

	[Fact]
	public void TypeParam_Is_Wrong_Type_Returns_None()
	{
		// Arrange
		var test = new Test(Rnd.Str);

		// Act
		var result = test.GetPropertyValue<int>(nameof(Test.Foo));

		// Assert
		result.AssertNone();
	}

	[Theory]
	[InlineData(null)]
	public void Value_Is_Null_Returns_None_With_NullValueMsg(string? input)
	{
		// Arrange
		var test = new Test(input);

		// Act
		var r0 = test.GetPropertyValue(nameof(Test.Foo));
		var r1 = test.GetPropertyValue<string>(nameof(Test.Foo));

		// Assert
		r0.AssertNone();
		r1.AssertNone();
	}

	[Fact]
	public void Returns_Property_Value()
	{
		// Arrange
		var value = Rnd.Str;
		var test = new Test(value);

		// Act
		var r0 = test.GetPropertyValue(nameof(Test.Foo));
		var r1 = test.GetPropertyValue<string>(nameof(Test.Foo));

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(value, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(value, s1);
	}

	public sealed record class Test(string? Foo);
}
