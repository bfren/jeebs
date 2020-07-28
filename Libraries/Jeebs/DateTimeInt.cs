using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// DateTime Integer
	/// </summary>
	public sealed class DateTimeInt
	{
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
		public DateTimeInt(int year, int month, int day, int hour, int minute)
			=> Init(year, month, day, hour, minute);

		/// <summary>
		/// Construct object using a DateTime object
		/// </summary>
		/// <param name="dt">DateTime</param>
		public DateTimeInt(DateTime dt)
			=> Init(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);

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
				throw new ArgumentException("DateTimeInt value must be a 12 characters long", nameof(value));
			}

			if (!long.TryParse(value, out _))
			{
				throw new ArgumentException($"{nameof(value)} is not a valid long");
			}

			Year = GetPart(value, 0, 4);
			Month = GetPart(value, 4, 2);
			Day = GetPart(value, 6, 2);
			Hour = GetPart(value, 8, 2);
			Minute = GetPart(value, 10, 2);
		}

		/// <summary>
		/// Construct object from long - must be exactly 12 digits long (yyyymmddHHMM)
		/// </summary>
		/// <param name="value">DateTime long value - format yyyymmddHHMM</param>
		public DateTimeInt(long value) : this(value.ToString()) { }

		/// <summary>
		/// Returns true if the object is a valid DateTime
		/// </summary>
		/// <returns>True if object is a valid DateTime</returns>
		public bool IsValidDateTime()
		{
			if (Year < 1 || Year > 9999)
			{
				return false;
			}

			if (Month < 1 || Month > 12)
			{
				return false;
			}

			if (Day < 1)
			{
				return false;
			}

			if (new[] { 1, 3, 5, 7, 8, 10, 12 }.Contains(Month) && Day > 31)
			{
				return false;
			}

			if (new[] { 4, 6, 9, 11 }.Contains(Month) && Day > 30)
			{
				return false;
			}

			if (Day > 29)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Returns true if the object is a valid DateTime
		/// Also outputs the object's valid as a DateTime
		/// </summary>
		/// <param name="dt">[Output] DateTime</param>
		/// <returns>True if the object is a valid DateTime</returns>
		public bool IsValidDateTime(out DateTime? dt)
		{
			if (!IsValidDateTime())
			{
				dt = null;
				return false;
			}

			dt = new DateTime(Year, Month, Day, Hour, Minute, 0);
			return true;
		}

		/// <summary>
		/// Get part of a string
		/// </summary>
		/// <param name="val">String value</param>
		/// <param name="start">Substring start</param>
		/// <param name="length">Substring length</param>
		/// <returns>Integer parsed part of a string</returns>
		private int GetPart(string val, int start, int length)
			=> int.Parse(val.Substring(start, length));

		/// <summary>
		/// Outputs object values as correctly formatted string
		/// If the object is not valid, returns an empty string
		/// </summary>
		/// <returns>String value of object, or empty string</returns>
		public override string ToString()
			=> IsValidDateTime() ? $"{Year:0000}{Month:00}{Day:00}{Hour:00}{Minute:00}" : string.Empty;

		/// <summary>
		/// Outputs object values as long
		/// If the object is not valid, returns 0
		/// </summary>
		/// <returns>Long value of object, or zero</returns>
		public long ToLong()
			=> IsValidDateTime() ? long.Parse(ToString()) : 0;
	}
}
