// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
				$"BEGIN:VCALENDAR{Environment.NewLine}" +
				$"VERSION:2.0{Environment.NewLine}" +
				$"PRODID:-//bfren//NONSGML Jeebs.Mvc.Calendar//EN{Environment.NewLine}" +
				$"CALSCALE:GREGORIAN{Environment.NewLine}" +
				$"X-PUBLISHED-TTL:PT1H{Environment.NewLine}" +
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
				$"END:VTIMEZONE{Environment.NewLine}" +
				$"BEGIN:VEVENT{Environment.NewLine}" +
				$"UID:{lastModifiedStr}-000000@{domain}{Environment.NewLine}" +
				$"CREATED:{createdStr}{Environment.NewLine}" +
				$"LAST-MODIFIED:{lastModifiedStr}{Environment.NewLine}" +
				$"DTSTAMP:{lastModifiedStr}{Environment.NewLine}" +
				$"SUMMARY:{e0.Summary}{Environment.NewLine}" +
				$"DESCRIPTION:{e0.Description}{Environment.NewLine}" +
				$"LOCATION:{e0.Location}{Environment.NewLine}" +
				$"DTSTART;TZID={tzid}:{VCalendar.Format(e0.Start)}{Environment.NewLine}" +
				$"DTEND;TZID={tzid}:{VCalendar.Format(e0.End)}{Environment.NewLine}" +
				$"END:VEVENT{Environment.NewLine}" +
				$"BEGIN:VEVENT{Environment.NewLine}" +
				$"UID:{lastModifiedStr}-000001@{domain}{Environment.NewLine}" +
				$"CREATED:{createdStr}{Environment.NewLine}" +
				$"LAST-MODIFIED:{lastModifiedStr}{Environment.NewLine}" +
				$"DTSTAMP:{lastModifiedStr}{Environment.NewLine}" +
				$"SUMMARY:{e1.Summary}{Environment.NewLine}" +
				$"DESCRIPTION:{e1.Description}{Environment.NewLine}" +
				$"LOCATION:{e1.Location}{Environment.NewLine}" +
				$"DTSTART;TZID={tzid}:{VCalendar.Format(e1.Start)}{Environment.NewLine}" +
				$"DTEND;TZID={tzid}:{VCalendar.Format(e1.End)}{Environment.NewLine}" +
				$"END:VEVENT{Environment.NewLine}" +
				$"END:VCALENDAR{Environment.NewLine}";

			// Act
			var result = vcal.ToString(domain);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
