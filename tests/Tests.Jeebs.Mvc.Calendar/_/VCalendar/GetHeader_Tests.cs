// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Mvc.Calendar.VCalendar_Tests
{
	public class GetHeader_Tests
	{
		[Fact]
		public void Returns_Correct_Header_Definition()
		{
			// Arrange
			var expected =
				"BEGIN:VCALENDAR\r\n" +
				"VERSION:2.0\r\n" +
				"PRODID:-//bcg|design//NONSGML Jeebs.Mvc.Calendar//EN\r\n" +
				"CALSCALE:GREGORIAN\r\n" +
				"X-PUBLISHED-TTL:PT1H\r\n";

			// Act
			var result = VCalendar.GetHeader();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
