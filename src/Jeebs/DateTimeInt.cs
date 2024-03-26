// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;
using System.Linq;

namespace Jeebs;

/// <summary>
/// DateTime Integer
/// </summary>
public sealed record class DateTimeInt
{
	/// <summary>
	/// Twelve numbers: YYYYMMDDhhmm
	/// </summary>
	private const string FormatString = "000000000000";

	/// <summary>
	/// January, March, May, July, August, October, December
	/// </summary>
	private static readonly int[] ThirtyOneDayMonths = [1, 3, 5, 7, 8, 10, 12];

	/// <summary>
	/// April, June, September, November
	/// </summary>
	private static readonly int[] ThirtyDayMonths = [4, 6, 9, 11];

	/// <summary>
	/// Year
	/// </summary>
	public int Year { get; init; }

	/// <summary>
	/// Month
	/// </summary>
	public int Month { get; init; }

	/// <summary>
	/// Day
	/// </summary>
	public int Day { get; init; }

	/// <summary>
	/// Hour
	/// </summary>
	public int Hour { get; init; }

	/// <summary>
	/// Minute
	/// </summary>
	public int Minute { get; init; }

	/// <summary>
	/// Construct using zero values
	/// </summary>
	public DateTimeInt() =>
		(Year, Month, Day, Hour, Minute) = (0, 0, 0, 0, 0);

	/// <summary>
	/// Construct object using specified date/time integers
	/// </summary>
	/// <param name="year">Year</param>
	/// <param name="month">Month</param>
	/// <param name="day">Day</param>
	/// <param name="hour">Hour</param>
	/// <param name="minute">Minute</param>
	public DateTimeInt(int year, int month, int day, int hour, int minute) =>
		(Year, Month, Day, Hour, Minute) = (year, month, day, hour, minute);

	/// <summary>
	/// Construct object using a DateTime object
	/// </summary>
	/// <param name="dt">DateTime</param>
	public DateTimeInt(DateTime dt) : this(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute) { }

	/// <summary>
	/// Construct object from string - must be exactly 12 characters long (yyyymmddHHMM)
	/// </summary>
	/// <param name="value">DateTime string value - format yyyymmddHHMM</param>
	public DateTimeInt(string value) =>
		(Year, Month, Day, Hour, Minute) = Parse(value);

	/// <summary>
	/// Construct object from long - will be converted to a 12-digit string with leading zeroes
	/// </summary>
	/// <param name="value">DateTime long value - format yyyymmddHHMM</param>
	public DateTimeInt(long value)
	{
		if (value <= 100000000000)
		{
			throw new ArgumentException("Too small - must be 12 digits long", nameof(value));
		}

		if (value > 999912312359) // the maximum allowed value - just before midnight on 31 December 9999
		{
			throw new ArgumentException("Too large - cannot be later than the year 9999", nameof(value));
		}

		(Year, Month, Day, Hour, Minute) = Parse(value.ToString(FormatString, CultureInfo.InvariantCulture));
	}

	/// <summary>
	/// Get the current DateTime
	/// </summary>
	public Result<DateTime> ToDateTime() =>
		IsValidDateTime() switch
		{
			{ } x when x.Valid =>
				new DateTime(Year, Month, Day, Hour, Minute, 0),

			{ } x =>
				Failures.InvalidDateTime(x.Part, this)
		};

	/// <summary>
	/// Outputs object values as correctly formatted string
	/// If the object is not valid, returns a string of zeroes
	/// </summary>
	public override string ToString() =>
		IsValidDateTime().Valid switch
		{
			true =>
				$"{Year:0000}{Month:00}{Day:00}{Hour:00}{Minute:00}",

			false =>
				0.ToString(FormatString, CultureInfo.InvariantCulture)
		};

	/// <summary>
	/// Outputs object values as long
	/// If the object is not valid, returns 0
	/// </summary>
	public long ToLong() =>
		IsValidDateTime().Valid switch
		{
			true =>
				long.Parse(ToString(), CultureInfo.InvariantCulture),

			false =>
				0
		};

	internal (bool Valid, string Part) IsValidDateTime()
	{
		if (Year is < 0 or > 9999)
		{
			return (false, nameof(Year));
		}

		if (Month is < 1 or > 12)
		{
			return (false, nameof(Month));
		}

		if (Day < 1)
		{
			return (false, nameof(Day));
		}

		// January, March, May, July, August, October, December
		if (ThirtyOneDayMonths.Contains(Month) && Day > 31)
		{
			return (false, nameof(Day));
		}
		// April, June, September, November
		else if (ThirtyDayMonths.Contains(Month) && Day > 30)
		{
			return (false, nameof(Day));
		}
		// February
		else
		{
			if (IsLeapYear(Year) && Day > 29)
			{
				return (false, nameof(Day));
			}
			else if (Day > 28)
			{
				return (false, nameof(Day));
			}
		}

		if (Hour is < 0 or > 23)
		{
			return (false, nameof(Hour));
		}

		if (Minute is < 0 or > 59)
		{
			return (false, nameof(Minute));
		}

		return (true, string.Empty);
	}

	internal static bool IsLeapYear(int year)
	{
		if (year % 400 == 0)
		{
			return true;
		}

		if (year % 100 == 0)
		{
			return false;
		}

		return year % 4 == 0;
	}

	#region Static

	/// <summary>
	/// Minimum possible value
	/// </summary>
	public static DateTimeInt MinValue =>
		new(0, 0, 0, 0, 0);

	/// <summary>
	/// Maximum possible value
	/// </summary>
	public static DateTimeInt MaxValue =>
		new(9999, 12, 31, 23, 59);

	private static (int year, int month, int day, int hour, int minute) Parse(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return (0, 0, 0, 0, 0);
		}

		if (value.Length != 12)
		{
			throw new ArgumentException($"{nameof(DateTimeInt)} value must be a 12 characters long", nameof(value));
		}

		if (!ulong.TryParse(value, out _))
		{
			throw new ArgumentException("Not a valid number", nameof(value));
		}

		return (
			year: int.Parse(value[0..4], CultureInfo.InvariantCulture),
			month: int.Parse(value[4..6], CultureInfo.InvariantCulture),
			day: int.Parse(value[6..8], CultureInfo.InvariantCulture),
			hour: int.Parse(value[8..10], CultureInfo.InvariantCulture),
			minute: int.Parse(value[10..], CultureInfo.InvariantCulture)
		);
	}

	#endregion Static

	/// <summary><see cref="DateTimeInt"/> Failures.</summary>
	public static class Failures
	{
		/// <summary>Unable to parse DateTime integer.</summary>
		/// <param name="part">The name of the part that caused the error.</param>
		/// <param name="dt">DateTime value.</param>
		public static Result<DateTime> InvalidDateTime(string part, DateTimeInt dt) =>
			R.Fail<DateTimeInt>(
				"Invalid {Part} - 'Y:{Year} M:{Month} D:{Day} H:{Hour} m:{Minute}'.",
				part, dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute
			);
	}
}
