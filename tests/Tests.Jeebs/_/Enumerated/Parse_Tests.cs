// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using MaybeF;

namespace Jeebs.Enumerated_Tests;

public class Parse_Tests
{
	[Fact]
	public void ValidString_Returns_Some()
	{
		// Arrange
		var input = EnumeratedTest.Test1.ToString();

		// Act
		var result = EnumeratedTest.Parse(input);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(EnumeratedTest.Test1, some);
	}

	[Fact]
	public void InvalidString_Returns_None()
	{
		// Arrange
		var input = Rnd.Str;

		// Act
		var result = EnumeratedTest.Parse(input);

		// Assert
		result.AssertNone();
	}
}

public record class EnumeratedTest : Enumerated
{
	public EnumeratedTest(string name) : base(name) { }

	public static readonly EnumeratedTest Test1 = new("test1");
	public static readonly EnumeratedTest Test2 = new("test2");

	public static Maybe<EnumeratedTest> Parse(string value)
	{
		return Parse(value, [Test1, Test2]);
	}
}
