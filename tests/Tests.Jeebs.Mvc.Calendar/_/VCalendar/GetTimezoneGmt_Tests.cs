// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Mvc.Calendar.VCalendar_Tests
{
	public class GetTimezoneGmt_Tests
	{
		[Fact]
		public void Returns_Correct_Timezone_Definition()
		{
			// Arrange
			var expected = 
				"BEGIN:VTIMEZONE\r\n" +
				"TZID:Europe/London\r\n" +
				"BEGIN:STANDARD\r\n" +
				"TZNAME:GMT\r\n" +
				"DTSTART:19710101T020000\r\n" +
				"TZOFFSETFROM:+0100\r\n" +
				"TZOFFSETTO:+0000\r\n" +
				"RRULE:FREQ=YEARLY;BYMONTH=10;BYDAY=-1SU\r\n" +
				"END:STANDARD\r\n" +
				"BEGIN:DAYLIGHT\r\n" +
				"TZNAME:BST\r\n" +
				"DTSTART:19710101T010000\r\n" +
				"TZOFFSETFROM:+0000\r\n" +
				"TZOFFSETTO:+0100\r\n" +
				"RRULE:FREQ=YEARLY;BYMONTH=3;BYDAY=-1SU\r\n" +
				"END:DAYLIGHT\r\n" +
				"END:VTIMEZONE\r\n";

			// Act
			var result = VCalendar.GetTimezoneGmt();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
