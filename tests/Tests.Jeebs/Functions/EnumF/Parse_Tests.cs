// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Xunit;

namespace F.EnumF_Tests;

public partial class Parse_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void NullOrEmpty_Returns_None(string input)
	{
		// Arrange

		// Act
		var result = EnumF.Parse<TestA>(input);

		// Assert
		_ = result.AssertNone();
	}

	[Fact]
	public void ValidValue_CorrectType_Returns_Some()
	{
		// Arrange
		const string input = nameof(TestA.Test1);

		// Act
		var result = EnumF.Parse<TestA>(input);

		// Assert
		Assert.Equal(TestA.Test1, result);
	}

	[Fact]
	public void InvalidValue_CorrectType_Returns_None()
	{
		// Arrange
		var input = Rnd.Str;

		// Act
		var result = EnumF.Parse<TestA>(input);

		// Assert
		_ = result.AssertNone();
	}

	[Fact]
	public void ValidValue_IncorrectType_Returns_None()
	{
		// Arrange
		const string input = nameof(TestA.Test1);

		// Act
		var result = EnumF.Parse(typeof(string), input);

		// Assert
		_ = result.AssertNone();
	}

	public enum TestA
	{
		Test1,
		Test2
	}

	public enum TestB
	{
		Test3,
		Test4,
		Test5
	}
}
