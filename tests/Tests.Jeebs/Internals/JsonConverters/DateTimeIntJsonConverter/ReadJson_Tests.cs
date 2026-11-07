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

		[Theory]
		[InlineData(1, 1, 1, 0, 0)]
		[InlineData(999, 12, 31, 12, 0)]
		public void Year_Before_1000__Returns_Value(int year, int month, int day, int hour, int minute)
		{
			// Arrange - these serialise with a leading zero, e.g. 099912311200
			var opt = GetOptions();
			var input = new DateTimeInt(year, month, day, hour, minute);
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
