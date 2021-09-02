// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.DateTimeExtensions_Tests
{
	public class EndOfDay_Tests
	{
		[Fact]
		public void Date_ReturnsOneMinuteToMidnight()
		{
			// Arrange
			var date = new DateTime(2000, 1, 1, 15, 59, 30);
			var expected = new DateTime(2000, 1, 1, 23, 59, 59);

			// Act
			var actual = date.EndOfDay();

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
