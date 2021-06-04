// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Mvc.Calendar.VCalendar_Tests
{
	public class Format_Tests
	{
		[Fact]
		public void Returns_Date_With_Time()
		{
			// Arrange
			var dt = F.Rnd.DateTime;
			var expected = dt.ToString(@"yyyyMMdd\THHmmss");

			// Act
			var result = VCalendar.Format(dt);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Returns_Date_Without_Time()
		{
			// Arrange
			var dt = F.Rnd.DateTime;
			var expected = dt.ToString("yyyyMMdd");

			// Act
			var result = VCalendar.Format(dt, false);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
