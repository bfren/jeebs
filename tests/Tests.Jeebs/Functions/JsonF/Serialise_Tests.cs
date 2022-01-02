// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace F.JsonF_Tests;

public class Serialise_Tests
{
	[Theory]
	[InlineData(null)]
	public void Null_Returns_Empty(object input)
	{
		// Arrange

		// Act
		var result = JsonF.Serialise(input);

		// Assert
		Assert.Equal(JsonF.Empty, result);
	}

	[Fact]
	public void Object_Returns_Json()
	{
		// Arrange
		var v0 = Rnd.Str;
		var v1 = Rnd.Int;
		var input = new Test { Foo = v0, Bar = v1 };
		var expected = $"{{\"foo\":\"{v0}\",\"bar\":{v1},\"empty\":null}}";

		// Act
		var result = JsonF.Serialise(input);

		// Assert
		Assert.Equal(expected, result);
	}

	public class Test
	{
		public string Foo { get; set; } = string.Empty;

		public int Bar { get; set; }

		public string? Empty { get; set; }
	}
}
