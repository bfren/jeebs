// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using Jeebs.Calendar.Models;

namespace Jeebs.Calendar.VCalendar_Tests;

public class GetEvent_Tests
{
	[Fact]
	public void Returns_Correct_Event_Definition()
	{
		// Arrange
		var lastModified = Rnd.DateTime;
		var tzid = Rnd.Str;
		var uid = Rnd.Str;
		var e = new EventModel(Rnd.DateTime, Rnd.DateTime, false, Rnd.Str, Rnd.Str, Rnd.Str, false);
		var expected = new StringBuilder()
			.AppendLine("BEGIN:VEVENT")
			.AppendLine($"UID:{uid}")
			.AppendLine($"CREATED:{VCalendar.Format(lastModified)}")
			.AppendLine($"LAST-MODIFIED:{VCalendar.Format(lastModified)}")
			.AppendLine($"DTSTAMP:{VCalendar.Format(lastModified)}")
			.AppendLine($"SUMMARY:{e.Summary}")
			.AppendLine($"DESCRIPTION:{e.Description}")
			.AppendLine($"LOCATION:{e.Location}")
			.AppendLine($"DTSTART;TZID={tzid}:{VCalendar.Format(e.Start)}")
			.AppendLine($"DTEND;TZID={tzid}:{VCalendar.Format(e.End)}")
			.AppendLine("END:VEVENT")
			.ToString();

		// Act
		var result = VCalendar.GetEvent(lastModified, tzid, uid, e);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Returns_Correct_AllDay_Event_Definition()
	{
		// Arrange
		var lastModified = Rnd.DateTime;
		var tzid = Rnd.Str;
		var uid = Rnd.Str;
		var e = new EventModel(Rnd.DateTime, Rnd.DateTime, true, Rnd.Str, Rnd.Str, Rnd.Str, true);
		var expected = new StringBuilder()
			.AppendLine("BEGIN:VEVENT")
			.AppendLine($"UID:{uid}")
			.AppendLine($"CREATED:{VCalendar.Format(lastModified)}")
			.AppendLine($"LAST-MODIFIED:{VCalendar.Format(lastModified)}")
			.AppendLine($"DTSTAMP:{VCalendar.Format(lastModified)}")
			.AppendLine($"SUMMARY:{e.Summary}")
			.AppendLine($"DESCRIPTION:{e.Description}")
			.AppendLine($"LOCATION:{e.Location}")
			.AppendLine($"DTSTART;TZID={tzid};VALUE=DATE:{VCalendar.Format(e.Start, false)}")
			.AppendLine("TRANSP:TRANSPARENT")
			.AppendLine("END:VEVENT")
			.ToString();

		// Act
		var result = VCalendar.GetEvent(lastModified, tzid, uid, e);

		// Assert
		Assert.Equal(expected, result);
	}
}
