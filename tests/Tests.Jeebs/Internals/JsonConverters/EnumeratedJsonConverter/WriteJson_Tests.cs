// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Internals.JsonConverters.EnumeratedJsonConverter_Tests;

public class WriteJson_Tests : Setup
{
	[Fact]
	public void Serialise_Returns_Json_Value()
	{
		// Arrange
		var opt = GetOptions();
		var value = Rnd.Str;
		var enumerated = new EnumeratedTest(value);

		// Act
		var result = JsonF.Serialise(enumerated, opt);

		// Assert
		Assert.Equal($"\"{value}\"", result);
	}

	[Theory]
	[InlineData(null)]
	public void Serialise_Null_Returns_Empty_Json(Enumerated? input)
	{
		// Arrange
		var opt = GetOptions();

		// Act
		var result = JsonF.Serialise(input, opt);

		// Assert
		Assert.Equal(JsonF.Empty, result);
	}

	public record class EnumeratedTest : Enumerated
	{
		public EnumeratedTest(string name) : base(name) { }
	}
}
