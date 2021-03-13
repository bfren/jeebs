// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using Msg = Jeebs.DateTimeIntMsg;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// DateTime Integer
	/// </summary>
	public sealed class DateTimeInt
	{
		private const string format = "000000000000";

		/// <summary>
		/// Year
		/// </summary>
		public int Year { get; set; }

		/// <summary>
		/// Month
		/// </summary>
		public int Month { get; set; }

		/// <summary>
		/// Day
		/// </summary>
		public int Day { get; set; }

		/// <summary>
		/// Hour
		/// </summary>
		public int Hour { get; set; }

		/// <summary>
		/// Minute
		/// </summary>
		public int Minute { get; set; }

		/// <summary>
		/// Empty constructor
		/// </summary>
		public DateTimeInt() { }

		/// <summary>
		/// Construct object using specified date/time integers
		/// </summary>
		/// <param name="year">Year</param>
		/// <param name="month">Month</param>
		/// <param name="day">Day</param>
		/// <param name="hour">Hour</param>
		/// <param name="minute">Minute</param>
		public DateTimeInt(int year, int month, int day, int hour, int minute) =>
			Init(year, month, day, hour, minute);

		/// <summary>
		/// Construct object using a DateTime object
		/// </summary>
		/// <param name="dt">DateTime</param>
		public DateTimeInt(DateTime dt) =>
			Init(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);

		/// <summary>
		/// Construct object using a nullable DateTime object
		/// </summary>
		/// <param name="dt">Nullable DateTime</param>
		public DateTimeInt(DateTime? dt)
		{
			if (dt.HasValue)
			{
				Init(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute);
			}
		}

		/// <summary>
		/// Initialise class properties
		/// </summary>
		/// <param name="year">Year</param>
		/// <param name="month">Month</param>
		/// <param name="day">Day</param>
		/// <param name="hour">Hour</param>
		/// <param name="minute">Minute</param>
		private void Init(int year, int month, int day, int hour, int minute)
		{
			Year = year;
			Month = month;
			Day = day;
			Hour = hour;
			Minute = minute;
		}

		/// <summary>
		/// Construct object from string - must be exactly 12 characters long (yyyymmddHHMM)
		/// </summary>
		/// <param name="value">DateTime string value - format yyyymmddHHMM</param>
		public DateTimeInt(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}

			if (value.Length != 12)
			{
				throw new ArgumentException($"{nameof(DateTimeInt)} value must be a 12 characters long", nameof(value));
			}

			if (!long.TryParse(value, out _))
			{
				throw new ArgumentException("Not a valid number", nameof(value));
			}

			Init(value);
		}

		/// <summary>
		/// Construct object from long - will be converted to a 12-digit string with leading zeroes
		/// </summary>
		/// <param name="value">DateTime long value - format yyyymmddHHMM</param>
		public DateTimeInt(long value)
		{
			if (value <= 99999999999)
			{
				throw new ArgumentException("Too small - must be 12 digits long", nameof(value));
			}

			if (value > 999912312359) // the maximum allowed value - just before midnight on 31 December 9999
			{
				throw new ArgumentException("Too large - cannot be later than the year 9999", nameof(value));
			}

			Init(value.ToString(format));
		}

		private void Init(string value)
		{
			Year = int.Parse(value[0..4]);
			Month = int.Parse(value[4..6]);
			Day = int.Parse(value[6..8]);
			Hour = int.Parse(value[8..10]);
			Minute = int.Parse(value[10..]);
		}

		/// <summary>
		/// Get the current DateTime
		/// </summary>
		public Option<DateTime> ToDateTime() =>
			IsValidDateTime() switch
			{
				{ } x when x.Valid =>
					new DateTime(Year, Month, Day, Hour, Minute, 0),

				{ } x =>
					None<DateTime>(new Msg.InvalidDateTimeMsg((x.Part, this)))
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
					0.ToString(format)
			};

		/// <summary>
		/// Outputs object values as long
		/// If the object is not valid, returns 0
		/// </summary>
		public long ToLong() =>
			IsValidDateTime().Valid switch
			{
				true =>
					long.Parse(ToString()),

				false =>
					0
			};

		private (bool Valid, string Part) IsValidDateTime()
		{
			if (Year < 0 || Year > 9999)
			{
				return (false, nameof(Year));
			}

			if (Month < 1 || Month > 12)
			{
				return (false, nameof(Month));
			}

			if (Day < 1)
			{
				return (false, nameof(Day));
			}

			if (new[] { 1, 3, 5, 7, 8, 10, 12 }.Contains(Month) && Day > 31)
			{
				return (false, nameof(Day));
			}
			else if (new[] { 4, 6, 9, 11 }.Contains(Month) && Day > 30)
			{
				return (false, nameof(Day));
			}
			else // February is the only month left
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

			if (Hour < 0 || Hour > 23)
			{
				return (false, nameof(Hour));
			}

			if (Minute < 0 || Minute > 59)
			{
				return (false, nameof(Minute));
			}

			return (true, string.Empty);
		}

		static internal bool IsLeapYear(int year)
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
	}

	namespace DateTimeIntMsg
	{
		/// <summary>Unable to parse DateTime integer</summary>
		/// <param name="Value">Invalid part and DateTimeInt</param>
		public sealed record InvalidDateTimeMsg((string part, DateTimeInt dt) Value) :
			WithValueMsg<(string part, DateTimeInt dt)>()
		{
			/// <summary>Return message</summary>
			public override string ToString() =>
				$"Invalid {Value.part} - 'Y:{Value.dt.Year} M:{Value.dt.Minute} D:{Value.dt.Day} H:{Value.dt.Hour} m:{Value.dt.Minute}'.";
		}
	}
}
