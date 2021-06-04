// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Mvc.Calendar.Models;
using Xunit;

namespace Jeebs.Mvc.Calendar.VCalendar_Tests
{
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
			var expected =
				"BEGIN:VCALENDAR\r\n" +
				"VERSION:2.0\r\n" +
				"PRODID:-//bcg|design//NONSGML Jeebs.Mvc.Calendar//EN\r\n" +
				"CALSCALE:GREGORIAN\r\n" +
				"X-PUBLISHED-TTL:PT1H\r\n" +
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
				"END:VTIMEZONE\r\n" +
				"BEGIN:VEVENT\r\n" +
				$"UID:{lastModifiedStr}-000000@{domain}\r\n" +
				$"CREATED:{createdStr}\r\n" +
				$"LAST-MODIFIED:{lastModifiedStr}\r\n" +
				$"DTSTAMP:{lastModifiedStr}\r\n" +
				$"SUMMARY:{e0.Summary}\r\n" +
				$"DESCRIPTION:{e0.Description}\r\n" +
				$"LOCATION:{e0.Location}\r\n" +
				$"DTSTART;TZID={tzid}:{VCalendar.Format(e0.Start)}\r\n" +
				$"DTEND;TZID={tzid}:{VCalendar.Format(e0.End)}\r\n" +
				"END:VEVENT\r\n" +
				"BEGIN:VEVENT\r\n" +
				$"UID:{lastModifiedStr}-000001@{domain}\r\n" +
				$"CREATED:{createdStr}\r\n" +
				$"LAST-MODIFIED:{lastModifiedStr}\r\n" +
				$"DTSTAMP:{lastModifiedStr}\r\n" +
				$"SUMMARY:{e1.Summary}\r\n" +
				$"DESCRIPTION:{e1.Description}\r\n" +
				$"LOCATION:{e1.Location}\r\n" +
				$"DTSTART;TZID={tzid}:{VCalendar.Format(e1.Start)}\r\n" +
				$"DTEND;TZID={tzid}:{VCalendar.Format(e1.End)}\r\n" +
				"END:VEVENT\r\n" +
				"END:VCALENDAR\r\n";

			// Act
			var result = vcal.ToString(domain);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
