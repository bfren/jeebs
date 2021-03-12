// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using JeebsF;
using Xunit;

namespace Jeebs.DateTimeInt_Tests
{
	public class IsValidDateTime_Tests
	{
		[Fact]
		public void ValidDateTime_ReturnsDateTime()
		{
			// Arrange
			var input = new DateTime(2000, 1, 2, 3, 4, 0);

			// Act
			var dt = new DateTimeInt(input);
			var result = dt.ToDateTime();

			// Assert
			var some = Assert.IsType<Some<DateTime>>(result);
			Assert.Equal(input, some.Value);
		}

		[Theory]
		[InlineData(10000, 1, 2, 3, 4)]
		[InlineData(2000, 0, 2, 3, 4)]
		[InlineData(2000, 1, 0, 3, 4)]
		[InlineData(2000, -1, 1, 2, 3)]
		[InlineData(2000, 13, 1, 2, 3)]
		[InlineData(2000, 1, 32, 2, 3)]
		[InlineData(2001, 2, 29, 2, 3)]
		[InlineData(2000, 2, 30, 2, 3)]
		[InlineData(2004, 2, 30, 2, 3)]
		[InlineData(2100, 2, 29, 2, 3)]
		[InlineData(2000, 3, 32, 2, 3)]
		[InlineData(2000, 4, 31, 2, 3)]
		[InlineData(2000, 5, 32, 2, 3)]
		[InlineData(2000, 6, 31, 2, 3)]
		[InlineData(2000, 7, 32, 2, 3)]
		[InlineData(2000, 8, 32, 2, 3)]
		[InlineData(2000, 9, 31, 2, 3)]
		[InlineData(2000, 10, 32, 2, 3)]
		[InlineData(2000, 11, 31, 2, 3)]
		[InlineData(2000, 12, 32, 2, 3)]
		[InlineData(2000, 1, 1, 24, 30)]
		[InlineData(2000, 1, 1, 23, 60)]
		[InlineData(-2000, 1, 1, 12, 30)]
		[InlineData(2000, -1, 1, 12, 30)]
		[InlineData(2000, 1, -1, 12, 30)]
		[InlineData(2000, 1, 1, -12, 30)]
		[InlineData(2000, 1, 1, 12, -30)]
		public void IsValidDateTime_InvalidDateTime_ReturnsNull(int year, int month, int day, int hour, int minute)
		{
			// Arrange
			var input = new DateTimeInt(year, month, day, hour, minute);

			// Act
			var result = input.ToDateTime();

			// Assert
			var none = Assert.IsType<None<DateTime>>(result);
			Assert.True(none.Reason is Jm.DateTimeInt.InvalidDateTimeMsg);
		}
	}
}
