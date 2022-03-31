// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.JsonConverters.EnumeratedConverter_Tests;

public class WriteJson_Tests
{
	[Fact]
	public void Serialise_Returns_Json_Value()
	{
		// Arrange
		var value = Rnd.Str;
		var enumerated = new EnumeratedTest(value);

		// Act
		var result = JsonF.Serialise(enumerated);

		// Assert
		Assert.Equal($"\"{value}\"", result);
	}

	[Theory]
	[InlineData(null)]
	public void Serialise_Null_Returns_Empty_Json(Enumerated input)
	{
		// Arrange

		// Act
		var result = JsonF.Serialise(input);

		// Assert
		Assert.Equal(JsonF.Empty, result);
	}

	public record class EnumeratedTest : Enumerated
	{
		public EnumeratedTest(string name) : base(name) { }
	}
}
