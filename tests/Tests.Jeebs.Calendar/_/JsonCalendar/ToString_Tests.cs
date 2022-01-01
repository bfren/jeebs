// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Calendar.Models;
using Xunit;

namespace Jeebs.Calendar.JsonCalendar_Tests;

public class ToString_Tests
{
	[Fact]
	public void Returns_Json()
	{
		// Arrange
		var e0 = new EventModel(F.Rnd.DateTime, F.Rnd.DateTime, false, F.Rnd.Str, F.Rnd.Str, F.Rnd.Str, false);
		var e1 = new EventModel(F.Rnd.DateTime, F.Rnd.DateTime, false, F.Rnd.Str, F.Rnd.Str, F.Rnd.Str, true);
		var events = ImmutableList.Create(e0, e1);
		var lastModified = F.Rnd.DateTime;
		var calendar = new CalendarModel(events, lastModified);
		var tzid = F.Rnd.Str;
		var jsonCalendar = new JsonCalendar(calendar, tzid);

		var expected =
			"{" +
				"\"calendar\":{" +
					"\"events\":{" +
						$"\"{lastModified:yyyyMMdd\\THHmmss}-000000\":{{" +
							$"\"start\":\"{e0.Start:s}\"," +
							$"\"end\":\"{e0.End:s}\"," +
							$"\"isAllDay\":{F.JsonF.Bool(e0.IsAllDay)}," +
							$"\"summary\":\"{e0.Summary}\"," +
							$"\"description\":\"{e0.Description}\"," +
							$"\"location\":\"{e0.Location}\"," +
							$"\"free\":{F.JsonF.Bool(e0.Free)}" +
						"}," +
						$"\"{lastModified:yyyyMMdd\\THHmmss}-000001\":{{" +
							$"\"start\":\"{e1.Start:s}\"," +
							$"\"end\":\"{e1.End:s}\"," +
							$"\"isAllDay\":{F.JsonF.Bool(e1.IsAllDay)}," +
							$"\"summary\":\"{e1.Summary}\"," +
							$"\"description\":\"{e1.Description}\"," +
							$"\"location\":\"{e1.Location}\"," +
							$"\"free\":{F.JsonF.Bool(e1.Free)}" +
						"}" +
					"}," +
					$"\"lastModified\":\"{lastModified:s}\"," +
					$"\"timezone\":\"{tzid}\"" +
				"}" +
			"}";

		// Act
		var result = jsonCalendar.ToString();

		// Assert
		Assert.Equal(expected, result);
	}
}
