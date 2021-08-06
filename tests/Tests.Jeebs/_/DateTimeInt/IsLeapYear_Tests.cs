// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.DateTimeInt_Tests
{
	public class IsLeapYear_Tests
	{
		[Theory]
		[InlineData(2000)]
		[InlineData(2004)]
		public void Is_Leap_Year_Returns_True(int year)
		{
			// Arrange

			// Act
			var result = DateTimeInt.IsLeapYear(year);

			// Assert
			Assert.True(result);
		}

		[Theory]
		[InlineData(2001)]
		[InlineData(2002)]
		[InlineData(2003)]
		[InlineData(2100)]
		public void Is_Not_Leap_Year_Returns_False(int year)
		{
			// Arrange

			// Act
			var result = DateTimeInt.IsLeapYear(year);

			// Assert
			Assert.False(result);
		}
	}
}
