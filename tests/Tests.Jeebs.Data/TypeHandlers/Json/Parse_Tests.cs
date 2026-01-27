// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;

namespace Jeebs.Data.TypeHandlers.Json_Tests;

public class Parse_Tests
{
	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData("\n")]
	public void Null_Or_Whitespace_Throws_JsonException(string input)
	{
		// Arrange
		var handler = new JsonTypeHandler<Test>();

		// Act
		var action = void () => handler.Parse(input);

		// Assert
		Assert.Throws<JsonException>(action);
	}

	[Fact]
	public void InvalidJson_Throws_JsonException()
	{
		// Arrange
		var handler = new JsonTypeHandler<Test>();
		var input = Rnd.Str;

		// Act
		var action = void () => handler.Parse(input);

		// Assert
		Assert.Throws<JsonException>(action);
	}

	[Fact]
	public void ValidJson_ReturnsObject()
	{
		// Arrange
		var handler = new JsonTypeHandler<Test>();
		var v0 = Rnd.Str;
		var v1 = Rnd.Int;
		var input = $"{{\"foo\":\"{v0}\",\"bar\":{v1},\"ignore\":\"this\"}}";
		var expected = new Test { Foo = v0, Bar = v1 };

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Equal(expected, result);
	}

	public record class Test
	{
		public string Foo { get; set; } = string.Empty;

		public int Bar { get; set; }

		public string? Empty { get; set; }
	}
}
