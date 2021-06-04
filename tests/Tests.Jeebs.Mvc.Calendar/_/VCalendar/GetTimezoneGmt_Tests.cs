// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
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
				$"BEGIN:VTIMEZONE{Environment.NewLine}" +
				$"TZID:Europe/London{Environment.NewLine}" +
				$"BEGIN:STANDARD{Environment.NewLine}" +
				$"TZNAME:GMT{Environment.NewLine}" +
				$"DTSTART:19710101T020000{Environment.NewLine}" +
				$"TZOFFSETFROM:+0100{Environment.NewLine}" +
				$"TZOFFSETTO:+0000{Environment.NewLine}" +
				$"RRULE:FREQ=YEARLY;BYMONTH=10;BYDAY=-1SU{Environment.NewLine}" +
				$"END:STANDARD{Environment.NewLine}" +
				$"BEGIN:DAYLIGHT{Environment.NewLine}" +
				$"TZNAME:BST{Environment.NewLine}" +
				$"DTSTART:19710101T010000{Environment.NewLine}" +
				$"TZOFFSETFROM:+0000{Environment.NewLine}" +
				$"TZOFFSETTO:+0100{Environment.NewLine}" +
				$"RRULE:FREQ=YEARLY;BYMONTH=3;BYDAY=-1SU{Environment.NewLine}" +
				$"END:DAYLIGHT{Environment.NewLine}" +
				$"END:VTIMEZONE{Environment.NewLine}";

			// Act
			var result = VCalendar.GetTimezoneGmt();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
