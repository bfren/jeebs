// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text;
using Jeebs.Calendar.Models;
using Xunit;

namespace Jeebs.Calendar.VCalendar_Tests;

public class ToString_Tests
{
	[Fact]
	public void Returns_Correct_VCalendar()
	{
		// Arrange
		var e0 = new EventModel(F.Rnd.DateTime, F.Rnd.DateTime, false, F.Rnd.Str, F.Rnd.Str, F.Rnd.Str);
		var e1 = new EventModel(F.Rnd.DateTime, F.Rnd.DateTime, false, F.Rnd.Str, F.Rnd.Str, F.Rnd.Str);
		var events = ImmutableList.Create(e0, e1);
		var lastModified = F.Rnd.DateTime;
		var lastModifiedStr = VCalendar.Format(lastModified);
		var createdStr = VCalendar.Format(DateTime.Now);
		var calendar = new CalendarModel(events, lastModified);
		var tzid = F.Rnd.Str;
		var domain = F.Rnd.Str;
		var vcal = new VCalendar(calendar, tzid);
		var expected = new StringBuilder()
			.AppendLine("BEGIN:VCALENDAR")
			.AppendLine("VERSION:2.0")
			.AppendLine("PRODID:-//bfren//NONSGML Jeebs.Calendar//EN")
			.AppendLine("CALSCALE:GREGORIAN")
			.AppendLine("X-PUBLISHED-TTL:PT1H")
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
			.AppendLine("BEGIN:VEVENT")
			.AppendLine($"UID:{lastModifiedStr}-000000@{domain}")
			.AppendLine($"CREATED:{createdStr}")
			.AppendLine($"LAST-MODIFIED:{lastModifiedStr}")
			.AppendLine($"DTSTAMP:{lastModifiedStr}")
			.AppendLine($"SUMMARY:{e0.Summary}")
			.AppendLine($"DESCRIPTION:{e0.Description}")
			.AppendLine($"LOCATION:{e0.Location}")
			.AppendLine($"DTSTART;TZID={tzid}:{VCalendar.Format(e0.Start)}")
			.AppendLine($"DTEND;TZID={tzid}:{VCalendar.Format(e0.End)}")
			.AppendLine("END:VEVENT")
			.AppendLine("BEGIN:VEVENT")
			.AppendLine($"UID:{lastModifiedStr}-000001@{domain}")
			.AppendLine($"CREATED:{createdStr}")
			.AppendLine($"LAST-MODIFIED:{lastModifiedStr}")
			.AppendLine($"DTSTAMP:{lastModifiedStr}")
			.AppendLine($"SUMMARY:{e1.Summary}")
			.AppendLine($"DESCRIPTION:{e1.Description}")
			.AppendLine($"LOCATION:{e1.Location}")
			.AppendLine($"DTSTART;TZID={tzid}:{VCalendar.Format(e1.Start)}")
			.AppendLine($"DTEND;TZID={tzid}:{VCalendar.Format(e1.End)}")
			.AppendLine("END:VEVENT")
			.AppendLine("END:VCALENDAR")
			.ToString();

		// Act
		var result = vcal.ToString(domain);

		// Assert
		Assert.Equal(expected, result);
	}
}
