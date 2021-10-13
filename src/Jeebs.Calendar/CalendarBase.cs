// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Calendar.Models;

namespace Jeebs.Calendar;

/// <summary>
/// Provides base calendar functionality
/// </summary>
public abstract class CalendarBase
{
	/// <summary>
	/// The default time zone, only used if none is specified
	/// </summary>
	public const string DefaultTimezone = "Europe/London";

	/// <summary>
	/// The calendar
	/// </summary>
	internal readonly CalendarModel calendar;

	/// <summary>
	/// The calendar's timezone (default: see <see cref="DefaultTimezone"/>)
	/// </summary>
	internal readonly string tzid;

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="calendar">Calendar</param>
	/// <param name="tzid">Timezone</param>
	protected CalendarBase(CalendarModel calendar, string tzid) =>
		(this.calendar, this.tzid) = (calendar, tzid);
}
