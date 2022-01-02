// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Calendar.Models;
using NSubstitute;
using Xunit;

namespace Jeebs.Calendar.JsonCalendar_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_LastModified_Using_Calendar_LastModified()
	{
		// Arrange
		var lastModified = F.Rnd.DateTime;
		var calendar = new CalendarModel
		{
			Events = Substitute.For<IImmutableList<EventModel>>(),
			LastModified = lastModified
		};

		// Act
		var result = new JsonCalendar(calendar);

		// Assert
		Assert.Equal(lastModified, result.LastModified);
	}

	[Fact]
	public void Without_Tzid_Uses_DefaultTimeZone()
	{
		// Arrange
		var calendar = new CalendarModel
		{
			Events = Substitute.For<IImmutableList<EventModel>>(),
			LastModified = F.Rnd.DateTime
		};

		// Act
		var result = new JsonCalendar(calendar);

		// Assert
		Assert.Equal(CalendarBase.DefaultTimezone, result.Timezone);
	}

	[Fact]
	public void With_Tzid_Sets_Timezone()
	{
		// Arrange
		var tzid = F.Rnd.Str;
		var calendar = new CalendarModel
		{
			Events = Substitute.For<IImmutableList<EventModel>>(),
			LastModified = F.Rnd.DateTime
		};

		// Act
		var result = new JsonCalendar(calendar, tzid);

		// Assert
		Assert.Equal(tzid, result.Timezone);
	}

	[Fact]
	public void Adds_Events_With_Incrementing_Uid()
	{
		// Arrange
		var e0 = new EventModel(F.Rnd.DateTime, F.Rnd.DateTime, false, F.Rnd.Str, F.Rnd.Str, F.Rnd.Str, false);
		var e1 = new EventModel(F.Rnd.DateTime, F.Rnd.DateTime, false, F.Rnd.Str, F.Rnd.Str, F.Rnd.Str, false);
		var e2 = new EventModel(F.Rnd.DateTime, F.Rnd.DateTime, false, F.Rnd.Str, F.Rnd.Str, F.Rnd.Str, false);
		var events = ImmutableList.Create(e0, e1, e2);
		var calendar = new CalendarModel(events, F.Rnd.DateTime);

		// Act
		var result = new JsonCalendar(calendar);

		// Assert
		Assert.Collection(result.Events,
			x =>
			{
				Assert.Equal('0', x.Key[^1]);
				Assert.Equal(e0, x.Value);
			},
			x =>
			{
				Assert.Equal('1', x.Key[^1]);
				Assert.Equal(e1, x.Value);
			},
			x =>
			{
				Assert.Equal('2', x.Key[^1]);
				Assert.Equal(e2, x.Value);
			}
		);
	}
}
