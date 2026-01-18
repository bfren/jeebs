// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Enumerated_Tests;

public class Parse_Tests
{
	[Fact]
	public void ValidString_Returns_Ok()
	{
		// Arrange
		var input = EnumeratedTest.Test1.ToString();

		// Act
		var result = EnumeratedTest.Parse(input);

		// Assert
		result.AssertOk(EnumeratedTest.Test1);
	}

	[Fact]
	public void InvalidString_Returns_Fail()
	{
		// Arrange
		var input = Rnd.Str;

		// Act
		var result = EnumeratedTest.Parse(input);

		// Assert
		result.AssertFail();
	}
}

public record class EnumeratedTest : Enumerated
{
	public EnumeratedTest(string name) : base(name) { }

	public static readonly EnumeratedTest Test1 = new("test1");
	public static readonly EnumeratedTest Test2 = new("test2");

	public static Result<EnumeratedTest> Parse(string value)
	{
		return Parse(value, [Test1, Test2]);
	}
}
