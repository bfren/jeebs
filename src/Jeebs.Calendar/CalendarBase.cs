// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Calendar.Models;

namespace Jeebs.Calendar;

/// <summary>
/// Provides base calendar functionality.
/// </summary>
public abstract class CalendarBase
{
	/// <summary>
	/// The default time zone, only used if none is specified.
	/// </summary>
	public static readonly string DefaultTimezone = "Europe/London";

	/// <summary>
	/// The calendar.
	/// </summary>
	internal CalendarModel Calendar { get; private init; }

	/// <summary>
	/// The calendar's timezone (default: see <see cref="DefaultTimezone"/>).
	/// </summary>
	internal string TzId { get; private init; }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="calendar">Calendar.</param>
	/// <param name="tzId">Timezone.</param>
	protected CalendarBase(CalendarModel calendar, string tzId) =>
		(Calendar, TzId) = (calendar, tzId);
}
