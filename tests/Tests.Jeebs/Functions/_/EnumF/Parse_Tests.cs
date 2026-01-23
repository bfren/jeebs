// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.EnumF_Tests;

public class Parse_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void NullOrEmpty_Returns_Fail(string? input)
	{
		// Arrange

		// Act
		var result = EnumF.Parse<TestA>(input);

		// Assert
		result.AssertFail("Attempting to parse a null value.");
	}

	[Fact]
	public void ValidValue_CorrectType_Returns_Ok()
	{
		// Arrange
		const string input = nameof(TestA.Test1);

		// Act
		var result = EnumF.Parse<TestA>(input);

		// Assert
		result.AssertOk(TestA.Test1);
	}

	[Fact]
	public void InvalidValue_CorrectType_Returns_Fail()
	{
		// Arrange
		var input = Rnd.Str;

		// Act
		var result = EnumF.Parse<TestA>(input);

		// Assert
		result.AssertFail(
			"'{Value}' is not a valid value of {Type}.",
			input, typeof(TestA).FullName
		);
	}

	[Fact]
	public void ValidValue_IncorrectType_Returns_Fail()
	{
		// Arrange
		const string input = nameof(TestA.Test1);

		// Act
		var result = EnumF.Parse<TestB>(input);

		// Assert
		result.AssertFail(
			"'{Value}' is not a valid value of {Type}.",
			input, typeof(TestB).FullName
		);
	}

	public enum TestA
	{
		Test1 = 0,
		Test2 = 1
	}

	public enum TestB
	{
		Test3 = 0,
		Test4 = 1,
		Test5 = 2
	}
}
