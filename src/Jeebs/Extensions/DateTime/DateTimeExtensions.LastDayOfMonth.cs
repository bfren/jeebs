// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public static partial class DateTimeExtensions
{
	/// <summary>
	/// Return one minute to midnight on the last day of the month for the specified date.
	/// </summary>
	/// <param name="this">DateTime object.</param>
	/// <returns>One second to midnight on the last day of the month specified by <paramref name="this"/>.</returns>
	public static DateTime LastDayOfMonth(this DateTime @this) =>
		new DateTime(@this.Year, @this.Month, 1, 23, 59, 59, @this.Kind).AddMonths(1).AddDays(-1);
}
