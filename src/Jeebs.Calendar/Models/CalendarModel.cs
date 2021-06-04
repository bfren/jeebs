// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Mvc.Calendar.Models
{
	/// <summary>
	/// Event Calendar
	/// </summary>
	/// <param name="Events">List of events</param>
	/// <param name="LastModified">Last modified date - for cache purposes</param>
	public sealed record CalendarModel(
		IImmutableList<EventModel> Events,
		DateTime LastModified
	);
}
