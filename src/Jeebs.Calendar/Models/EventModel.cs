// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Calendar.Models;

/// <summary>
/// Calendar Event
/// </summary>
/// <param name="Start">Event start date and time</param>
/// <param name="End">Event end date and time</param>
/// <param name="IsAllDay">True if this is an all-day event</param>
/// <param name="Summary">Event summary / title</param>
/// <param name="Description">Event description / details</param>
/// <param name="Location">Event location</param>
/// <param name="Free">True if the event shouldn't block time on the calendar</param>
public sealed record class EventModel(
	DateTime Start,
	DateTime End,
	bool IsAllDay,
	string Summary,
	string Description,
	string Location,
	bool Free
);
