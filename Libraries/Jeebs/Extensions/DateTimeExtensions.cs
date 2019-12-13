using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// DateTime Extensions
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Return midnight on the specified day
		/// </summary>
		/// <param name="dt">DateTime object</param>
		/// <returns>Start of the specified day</returns>
		public static DateTime StartOfDay(this DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);

		/// <summary>
		/// Return one second to midnight on the specified day
		/// </summary>
		/// <param name="dt">DateTime object</param>
		/// <returns>End of the specified day</returns>
		public static DateTime EndOfDay(this DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);

		/// <summary>
		/// Return midnight on the the first day of the week for the specified date
		/// </summary>
		/// <param name="dt">DateTime object</param>
		/// <returns>First day of the specified week</returns>
		public static DateTime FirstDayOfWeek(this DateTime dt)
		{
			// Get the day of the week and subtract from the current day
			int dayOfWeek = (int)dt.DayOfWeek;
			return dt.AddDays(dayOfWeek * -1).StartOfDay();
		}

		/// <summary>
		/// Return one minute to midnight on the last day of the week for the specified date
		/// </summary>
		/// <param name="dt">DateTime object</param>
		/// <returns>Last day of the specified week</returns>
		public static DateTime LastDayOfWeek(this DateTime dt)
		{
			// Get the day of the week and subtract from the current day
			int dayOfWeek = (int)dt.DayOfWeek;
			return dt.AddDays(6 - dayOfWeek).EndOfDay();
		}

		/// <summary>
		/// Return midnight on the first day of the month for the specified date
		/// </summary>
		/// <param name="dt">DateTime object</param>
		/// <returns>First day of the month, at midnight</returns>
		public static DateTime FirstDayOfMonth(this DateTime dt) => new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);

		/// <summary>
		/// Return one minute to midnight on the last day of the month for the specified date
		/// </summary>
		/// <param name="dt">DateTime object</param>
		/// <returns>Last day of the month, at one minute to midnight</returns>
		public static DateTime LastDayOfMonth(this DateTime dt) => new DateTime(dt.Year, dt.Month, 1, 23, 59, 59).AddMonths(1).AddDays(-1);

		/// <summary>
		/// Return a standard format date/time value (HH:mm dd/MM/yyyy)
		/// </summary>
		/// <param name="dt">DateTime object</param>
		/// <returns>Standard Formatted string</returns>
		public static string ToStandardString(this DateTime dt) => dt.ToString("HH:mm dd/MM/yyyy");
	}
}
