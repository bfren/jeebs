// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public static partial class DateTimeExtensions
{
	/// <summary>
	/// Return midnight on the first day of the week for the specified date.
	/// </summary>
	/// <param name="this">DateTime object.</param>
	/// <returns>Midnight on the first day of the week specified by <paramref name="this"/>.</returns>
	public static DateTime FirstDayOfWeek(this DateTime @this) =>
		@this.AddDays((int)@this.DayOfWeek * -1).StartOfDay();
}
