// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Calendar.Models
{
	/// <summary>
	/// Event Calendar
	/// </summary>
	/// <param name="Events">List of events</param>
	/// <param name="LastModified">Last modified date - for cache purposes</param>
	public readonly record struct CalendarModel(
		IImmutableList<EventModel> Events,
		DateTime LastModified
	);
}
