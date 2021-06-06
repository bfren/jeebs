// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs.Mvc.Calendar.Models;
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
				$"BEGIN:VEVENT{Environment.NewLine}" +
				$"UID:{uid}{Environment.NewLine}" +
				$"CREATED:{VCalendar.Format(DateTime.Now)}{Environment.NewLine}" +
				$"LAST-MODIFIED:{VCalendar.Format(lastModified)}{Environment.NewLine}" +
				$"DTSTAMP:{VCalendar.Format(lastModified)}{Environment.NewLine}" +
				$"SUMMARY:{e.Summary}{Environment.NewLine}" +
				$"DESCRIPTION:{e.Description}{Environment.NewLine}" +
				$"LOCATION:{e.Location}{Environment.NewLine}" +
				$"DTSTART;TZID={tzid}:{VCalendar.Format(e.Start)}{Environment.NewLine}" +
				$"DTEND;TZID={tzid}:{VCalendar.Format(e.End)}{Environment.NewLine}" +
				$"END:VEVENT{Environment.NewLine}";

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
				$"BEGIN:VEVENT{Environment.NewLine}" +
				$"UID:{uid}{Environment.NewLine}" +
				$"CREATED:{VCalendar.Format(DateTime.Now)}{Environment.NewLine}" +
				$"LAST-MODIFIED:{VCalendar.Format(lastModified)}{Environment.NewLine}" +
				$"DTSTAMP:{VCalendar.Format(lastModified)}{Environment.NewLine}" +
				$"SUMMARY:{e.Summary}{Environment.NewLine}" +
				$"DESCRIPTION:{e.Description}{Environment.NewLine}" +
				$"LOCATION:{e.Location}{Environment.NewLine}" +
				$"DTSTART;TZID={tzid};VALUE=DATE:{VCalendar.Format(e.Start, false)}{Environment.NewLine}" +
				$"END:VEVENT{Environment.NewLine}";

			// Act
			var result = VCalendar.GetEvent(lastModified, tzid, uid, e);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
