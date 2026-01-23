// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Calendar.Models;
using Jeebs.Collections;

namespace Jeebs.Calendar.VCalendar_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Without_Tzid_Uses_DefaultTimeZone()
	{
		// Arrange
		var events = Substitute.For<IImmutableList<EventModel>>();
		var calendar = new CalendarModel(events, Rnd.DateTime);

		// Act
		var result = new VCalendar(calendar);

		// Assert
		Assert.Equal(CalendarBase.DefaultTimezone, result.TzId);
	}

	[Fact]
	public void Sets_Tzid()
	{
		// Arrange
		var events = Substitute.For<IImmutableList<EventModel>>();
		var calendar = new CalendarModel(events, Rnd.DateTime);
		var tzid = Rnd.Str;

		// Act
		var result = new VCalendar(calendar, tzid);

		// Assert
		Assert.Equal(tzid, result.TzId);
	}

	[Fact]
	public void Sets_Calendar()
	{
		// Arrange
		var events = Substitute.For<IImmutableList<EventModel>>();
		var calendar = new CalendarModel(events, Rnd.DateTime);

		// Act
		var result = new VCalendar(calendar);

		// Assert
		Assert.Equal(calendar, result.Calendar);
	}
}
