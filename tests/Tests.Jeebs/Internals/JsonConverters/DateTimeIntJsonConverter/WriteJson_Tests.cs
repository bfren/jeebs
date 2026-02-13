// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Internals.JsonConverters.DateTimeIntJsonConverter_Tests;

public class WriteJson_Tests
{
	public class With_Value : Setup
	{
		[Fact]
		public void Returns_Json_Value()
		{
			// Arrange
			var opt = GetOptions();
			var value = Rnd.DateTime;
			var dtInt = new DateTimeInt(value);

			// Act
			var result = JsonF.Serialise(dtInt, opt);

			// Assert
			Assert.Equal($"\"{dtInt}\"", result);
		}
	}

	public class With_Null : Setup
	{
		[Theory]
		[InlineData(null)]
		public void Returns_Empty_Json(DateTimeInt? input)
		{
			// Arrange
			var opt = GetOptions();

			// Act
			var result = JsonF.Serialise(input, opt);

			// Assert
			Assert.Equal(JsonF.Empty, result);
		}
	}
}
