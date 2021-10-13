// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using Xunit;

namespace Jeebs.Calendar.VCalendar_Tests;

public class GetTimezoneGmt_Tests
{
	[Fact]
	public void Returns_Correct_Timezone_Definition()
	{
		// Arrange
		var expected = new StringBuilder()
			.AppendLine("BEGIN:VTIMEZONE")
			.AppendLine("TZID:Europe/London")
			.AppendLine("BEGIN:STANDARD")
			.AppendLine("TZNAME:GMT")
			.AppendLine("DTSTART:19710101T020000")
			.AppendLine("TZOFFSETFROM:+0100")
			.AppendLine("TZOFFSETTO:+0000")
			.AppendLine("RRULE:FREQ=YEARLY;BYMONTH=10;BYDAY=-1SU")
			.AppendLine("END:STANDARD")
			.AppendLine("BEGIN:DAYLIGHT")
			.AppendLine("TZNAME:BST")
			.AppendLine("DTSTART:19710101T010000")
			.AppendLine("TZOFFSETFROM:+0000")
			.AppendLine("TZOFFSETTO:+0100")
			.AppendLine("RRULE:FREQ=YEARLY;BYMONTH=3;BYDAY=-1SU")
			.AppendLine("END:DAYLIGHT")
			.AppendLine("END:VTIMEZONE")
			.ToString();

		// Act
		var result = VCalendar.GetTimezoneGmt();

		// Assert
		Assert.Equal(expected, result);
	}
}
