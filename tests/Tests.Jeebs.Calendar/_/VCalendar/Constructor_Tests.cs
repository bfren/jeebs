// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Calendar.Models;
using NSubstitute;
using Xunit;

namespace Jeebs.Calendar.VCalendar_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Default_Tzid_To_DefaultTimeZone()
		{
			// Arrange
			var events = Substitute.For<IImmutableList<EventModel>>();
			var calendar = new CalendarModel(events, F.Rnd.DateTime);

			// Act
			var result = new VCalendar(calendar);

			// Assert
			Assert.Equal(CalendarBase.DefaultTimeZone, result.tzid);
		}

		[Fact]
		public void Sets_Tzid()
		{
			// Arrange
			var events = Substitute.For<IImmutableList<EventModel>>();
			var calendar = new CalendarModel(events, F.Rnd.DateTime);
			var tzid = F.Rnd.Str;

			// Act
			var result = new VCalendar(calendar, tzid);

			// Assert
			Assert.Equal(tzid, result.tzid);
		}

		[Fact]
		public void Sets_Calendar()
		{
			// Arrange
			var events = Substitute.For<IImmutableList<EventModel>>();
			var calendar = new CalendarModel(events, F.Rnd.DateTime);

			// Act
			var result = new VCalendar(calendar);

			// Assert
			Assert.Equal(calendar, result.calendar);
		}
	}
}
