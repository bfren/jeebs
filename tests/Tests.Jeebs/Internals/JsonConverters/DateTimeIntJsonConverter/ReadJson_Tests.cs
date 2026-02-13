// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Internals.JsonConverters.DateTimeIntJsonConverter_Tests;

public class ReadJson_Tests : Setup
{
	public class With_Valid_Input : Setup
	{
		[Fact]
		public void Returns_Value()
		{
			// Arrange
			var opt = GetOptions();
			var input = new DateTimeInt(Rnd.DateTime);
			var json = $"{{\"Value\":\"{input}\"}}";

			// Act
			var result = JsonF.Deserialise<DateTimeIntWrapped>(json, opt);

			// Assert
			var ok = result.AssertOk();
			Assert.Equal(input, ok.Value);
		}
	}

	public class With_Invalid_Input : Setup
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("invalid")]
		public void Returns_MinValue(string? input)
		{
			// Arrange
			var opt = GetOptions();
			var json = $"{{\"Value\":\"{input}\"}}";

			// Act
			var result = JsonF.Deserialise<DateTimeIntWrapped>(json, opt);

			// Assert
			var ok = result.AssertOk();
			Assert.Equal(DateTimeInt.MinValue, ok.Value);
		}
	}

	public record DateTimeIntWrapped(DateTimeInt Value);
}
