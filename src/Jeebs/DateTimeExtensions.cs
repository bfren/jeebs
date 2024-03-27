// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs;

/// <summary>
/// Extension methods for <see cref="DateTime"/> objects.
/// </summary>
public static class DateTimeExtensions
{
	/// <summary>
	/// Return midnight on the specified day
	/// </summary>
	/// <param name="this">DateTime object</param>
	public static DateTime StartOfDay(this DateTime @this) =>
		new(@this.Year, @this.Month, @this.Day, 0, 0, 0, @this.Kind);

	/// <summary>
	/// Return one second to midnight on the specified day
	/// </summary>
	/// <param name="this">DateTime object</param>
	public static DateTime EndOfDay(this DateTime @this) =>
		new(@this.Year, @this.Month, @this.Day, 23, 59, 59, @this.Kind);

	/// <summary>
	/// Return midnight on the first day of the week for the specified date
	/// </summary>
	/// <param name="this">DateTime object</param>
	public static DateTime FirstDayOfWeek(this DateTime @this) =>
		@this.AddDays((int)@this.DayOfWeek * -1).StartOfDay();

	/// <summary>
	/// Return one minute to midnight on the last day of the week for the specified date
	/// </summary>
	/// <param name="this">DateTime object</param>
	public static DateTime LastDayOfWeek(this DateTime @this) =>
		@this.AddDays(6 - (int)@this.DayOfWeek).EndOfDay();

	/// <summary>
	/// Return midnight on the first day of the month for the specified date
	/// </summary>
	/// <param name="this">DateTime object</param>
	public static DateTime FirstDayOfMonth(this DateTime @this) =>
		new(@this.Year, @this.Month, 1, 0, 0, 0, @this.Kind);

	/// <summary>
	/// Return one minute to midnight on the last day of the month for the specified date
	/// </summary>
	/// <param name="this">DateTime object</param>
	public static DateTime LastDayOfMonth(this DateTime @this) =>
		new DateTime(@this.Year, @this.Month, 1, 23, 59, 59, @this.Kind).AddMonths(1).AddDays(-1);

	/// <summary>
	/// Return a standard format date/time value (HH:mm dd/MM/yyyy)
	/// </summary>
	/// <param name="this">DateTime object</param>
	public static string ToSortableString(this DateTime @this) =>
		@this.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffff", CultureInfo.InvariantCulture);

	/// <summary>
	/// Return a standard format date/time value (HH:mm dd/MM/yyyy)
	/// </summary>
	/// <param name="this">DateTime object</param>
	public static string ToStandardString(this DateTime @this) =>
		@this.ToString("HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture);
}
