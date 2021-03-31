// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Text.Json;
using Xunit;

namespace Jeebs.Data.TypeHandlers.Json_Tests
{
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
			void action() => handler.Parse(input);

			// Assert
			Assert.Throws<JsonException>(action);
		}

		[Fact]
		public void InvalidJson_Throws_JsonException()
		{
			// Arrange
			var handler = new JsonTypeHandler<Test>();
			var input = F.Rnd.Str;

			// Act
			void action() => handler.Parse(input);

			// Assert
			Assert.Throws<JsonException>(action);
		}

		[Fact]
		public void ValidJson_ReturnsObject()
		{
			// Arrange
			var handler = new JsonTypeHandler<Test>();
			var v0 = F.Rnd.Str;
			var v1 = F.Rnd.Int;
			var input = $"{{\"foo\":\"{v0}\",\"bar\":{v1},\"ignore\":\"this\"}}";
			var expected = new Test { Foo = v0, Bar = v1 };

			// Act
			var result = handler.Parse(input);

			// Assert
			Assert.Equal(expected, result);
		}

		public record Test
		{
			public string Foo { get; set; } = string.Empty;

			public int Bar { get; set; }

			public string? Empty { get; set; }
		}
	}
}
