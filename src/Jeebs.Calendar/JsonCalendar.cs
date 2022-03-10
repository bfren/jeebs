// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs.Calendar.Functions;
using Jeebs.Calendar.Models;
using Jeebs.Functions;

namespace Jeebs.Calendar;

/// <summary>
/// Export calendar model as JSON
/// </summary>
public class JsonCalendar : CalendarBase
{
	/// <inheritdoc cref="CalendarModel.Events"/>
	public Dictionary<string, EventModel> Events { get; internal init; }

	/// <inheritdoc cref="CalendarModel.LastModified"/>
	public DateTime LastModified { get; internal init; }

	/// <inheritdoc cref="CalendarBase.TzId"/>
	public string Timezone { get; internal init; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="calendar">Calendar</param>
	public JsonCalendar(CalendarModel calendar) : this(calendar, DefaultTimezone) { }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="calendar">Calendar</param>
	/// <param name="tzid">Timezone</param>
	public JsonCalendar(CalendarModel calendar, string tzid) : base(calendar, tzid)
	{
		// Set values
		(LastModified, Timezone) = (calendar.LastModified, tzid);

		// Add each event to the dictionary with a unique ID
		Events = new();
		var counter = 0;
		foreach (var e in calendar.Events)
		{
			var uid = CalendarF.GenerateEventUid(counter++, LastModified);
			Events.Add(uid, e);
		}
	}

	/// <inheritdoc/>
	public override string ToString() =>
		JsonF.Serialise(new { calendar = this }).Unwrap("{\"calendar\":{}}");
}
