// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.Mvc.Calendar.Models
{
	/// <summary>
	/// Event Calendar
	/// </summary>
	/// <param name="Events">List of events</param>
	/// <param name="LastModified">Last modified date - for cache purposes</param>
	public sealed record class CalendarModel(
		IImmutableList<EventModel> Events,
		DateTime LastModified
	);
}
