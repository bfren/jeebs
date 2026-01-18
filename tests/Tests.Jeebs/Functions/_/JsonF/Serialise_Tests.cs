// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Functions.JsonF_Tests.Deserialise_Tests;

namespace Jeebs.Functions.JsonF_Tests;

public class Serialise_Tests
{
	[Theory]
	[InlineData(null)]
	public void Null_Returns_Empty(object? input)
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
		var v0 = Rnd.Lng;
		var v1 = Rnd.Str;
		var v2 = Rnd.Int;
		var v3 = Rnd.DateTime;
		var v4 = Rnd.Flip;
		var input = new Test { Id = new(v0), Str = v1, Num = v2, DT = v3, Mbe = v4 };
		var expected =
			"{" +
			$"\"id\":\"{v0}\"," +
			$"\"str\":\"{v1}\"," +
			$"\"num\":{v2}," +
			$"\"dt\":\"{v3:s}\"," +
			$"\"mbe\":{JsonF.Bool(v4)}," +
			"\"empty\":null" +
			"}";

		// Act
		var result = JsonF.Serialise(input);

		// Assert
		Assert.Equal(expected, result);
	}
}
