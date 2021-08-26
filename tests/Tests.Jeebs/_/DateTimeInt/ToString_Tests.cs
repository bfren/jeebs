// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.DateTimeInt_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Valid_Returns_String_Value()
		{
			// Arrange
			const string expected = "200001020304";
			var input = new DateTimeInt(expected);

			// Act
			var result = input.ToString();

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[MemberData(nameof(IsValidDateTime_Tests.Invalid_DateTime_Data), MemberType = typeof(IsValidDateTime_Tests))]
#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
		public void Invalid_Returns_Zeroes(int year, int month, int day, int hour, int minute, string _)
#pragma warning restore xUnit1026 // Theory methods should use all of their parameters
		{
			// Arrange
			var input = new DateTimeInt(year, month, day, hour, minute);

			// Act
			var result = input.ToString();

			// Assert
			Assert.Equal("000000000000", result);
		}
	}
}
