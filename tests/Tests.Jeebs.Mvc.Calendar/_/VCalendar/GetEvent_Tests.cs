// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Mvc.Calendar.Models;
using NSubstitute;
using Xunit;

namespace Jeebs.Mvc.Calendar.VCalendar_Tests
{
	public class GetEvent_Tests
	{
		[Fact]
		public void Returns_Correct_Event_Definition()
		{
			// Arrange
			var lastModified = F.Rnd.DateTime;
			var tzid = F.Rnd.Str;
			var uid = F.Rnd.Str;
			var e = new EventModel(F.Rnd.DateTime, F.Rnd.DateTime, false, F.Rnd.Str, F.Rnd.Str, F.Rnd.Str);
			var expected =
				"BEGIN:VEVENT\r\n" +
				$"UID:{uid}\r\n" +
				$"CREATED:{VCalendar.Format(DateTime.Now)}\r\n" +
				$"LAST-MODIFIED:{VCalendar.Format(lastModified)}\r\n" +
				$"DTSTAMP:{VCalendar.Format(lastModified)}\r\n" +
				$"SUMMARY:{e.Summary}\r\n" +
				$"DESCRIPTION:{e.Description}\r\n" +
				$"LOCATION:{e.Location}\r\n" +
				$"DTSTART;TZID={tzid}:{VCalendar.Format(e.Start)}\r\n" +
				$"DTEND;TZID={tzid}:{VCalendar.Format(e.End)}\r\n" +
				"END:VEVENT\r\n";

			// Act
			var result = VCalendar.GetEvent(lastModified, tzid, uid, e);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Returns_Correct_AllDay_Event_Definition()
		{
			// Arrange
			var lastModified = F.Rnd.DateTime;
			var tzid = F.Rnd.Str;
			var uid = F.Rnd.Str;
			var e = new EventModel(F.Rnd.DateTime, F.Rnd.DateTime, true, F.Rnd.Str, F.Rnd.Str, F.Rnd.Str);
			var expected =
				"BEGIN:VEVENT\r\n" +
				$"UID:{uid}\r\n" +
				$"CREATED:{VCalendar.Format(DateTime.Now)}\r\n" +
				$"LAST-MODIFIED:{VCalendar.Format(lastModified)}\r\n" +
				$"DTSTAMP:{VCalendar.Format(lastModified)}\r\n" +
				$"SUMMARY:{e.Summary}\r\n" +
				$"DESCRIPTION:{e.Description}\r\n" +
				$"LOCATION:{e.Location}\r\n" +
				$"DTSTART;TZID={tzid};VALUE=DATE:{VCalendar.Format(e.Start, false)}\r\n" +
				"END:VEVENT\r\n";

			// Act
			var result = VCalendar.GetEvent(lastModified, tzid, uid, e);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
