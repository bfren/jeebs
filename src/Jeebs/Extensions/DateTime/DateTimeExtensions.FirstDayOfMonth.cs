// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public static partial class DateTimeExtensions
{
	/// <summary>
	/// Return midnight on the first day of the month for the specified date.
	/// </summary>
	/// <param name="this">DateTime object.</param>
	/// <returns>Midnight on the first day of the month specified by <paramref name="this"/>.</returns>
	public static DateTime FirstDayOfMonth(this DateTime @this) =>
		new(@this.Year, @this.Month, 1, 0, 0, 0, @this.Kind);
}
