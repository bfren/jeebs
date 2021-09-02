// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace F.JsonF_Tests
{
	public class Serialise_Tests
	{
		[Theory]
		[InlineData(null)]
		public void Null_ReturnsEmpty(object input)
		{
			// Arrange

			// Act
			var result = JsonF.Serialise(input);

			// Assert
			Assert.Equal(JsonF.Empty, result);
		}

		[Fact]
		public void Object_ReturnsJson()
		{
			// Arrange
			var v0 = Rnd.Str;
			var v1 = Rnd.Int;
			var input = new Test { Foo = v0, Bar = v1 };
			var expected = $"{{\"foo\":\"{v0}\",\"bar\":{v1}}}";

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
}
