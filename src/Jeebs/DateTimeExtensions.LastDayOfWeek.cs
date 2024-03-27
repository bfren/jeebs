// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public static partial class DateTimeExtensions
{
	/// <summary>
	/// Return one minute to midnight on the last day of the week for the specified date.
	/// </summary>
	/// <param name="this">DateTime object.</param>
	/// <returns>One second to midnight on the last day of the week specified by <paramref name="this"/>.</returns>
	public static DateTime LastDayOfWeek(this DateTime @this) =>
		@this.AddDays(6 - (int)@this.DayOfWeek).EndOfDay();
}
