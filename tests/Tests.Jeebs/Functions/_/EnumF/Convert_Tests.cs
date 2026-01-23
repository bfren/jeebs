// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.EnumF_Tests;

public class Convert_Tests
{
	[Fact]
	public void MatchingValue_ReturnsValue()
	{
		// Arrange
		const TestB input = TestB.Test3;

		// Act
		var result = EnumF.Convert(input).To<TestA>();

		// Assert
		Assert.Equal(TestA.Test1, result);
	}

	[Fact]
	public void NoMatchingValue_Returns_Fail()
	{
		// Arrange
		const TestB input = TestB.Test5;

		// Act
		var result = EnumF.Convert(input).To<TestA>();

		// Assert
		result.AssertFail(
			"'{Value}' is not a valid value of {Type}.",
			new { Value = nameof(TestB.Test5), Type = typeof(TestA).FullName }
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
