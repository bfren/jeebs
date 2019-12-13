using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Date Range
	/// </summary>
	public sealed class DateRange : IRange<DateTime>
	{
		/// <summary>
		/// Range start
		/// </summary>
		public DateTime Start { get; }

		/// <summary>
		/// Range end
		/// </summary>
		public DateTime End { get; }

		/// <summary>
		/// The length of the range
		/// </summary>
		public int Length { get => End.Subtract(Start).Days + 1; }

		/// <summary>
		/// Create range object from a single date
		/// </summary>
		/// <param name="single">Range start and end</param>
		public DateRange(DateTime single)
		{
			Start = single.StartOfDay();
			End = single.EndOfDay();
		}

		/// <summary>
		/// Create range object, making sure start is before end (!)
		/// Start and end are inclusive
		/// </summary>
		/// <param name="start">Range start</param>
		/// <param name="end">Range end</param>
		public DateRange(DateTime start, DateTime end)
		{
			if (start < end)
			{
				Start = start.StartOfDay();
				End = end.EndOfDay();
			}
			else
			{
				throw new ArgumentException($"{nameof(start)} must be before {nameof(end)}.", nameof(start));
			}
		}

		/// <summary>
		/// Whether or not the range includes the specified value
		/// </summary>
		/// <param name="value">IRange</param>
		/// <returns>True / false</returns>
		public bool Includes(DateTime value) => Start <= value && End >= value;

		/// <summary>
		/// Whether or not the range includes the specified range of values
		/// </summary>
		/// <param name="value">IRange</param>
		/// <returns>True / false</returns>
		public bool Includes(IRange<DateTime> value) => Start <= value.Start && End >= value.End;

		/// <summary>
		/// Whether or not the range overlaps the specified range
		/// </summary>
		/// <param name="value">IRange</param>
		/// <returns>True / false</returns>
		public bool Overlaps(IRange<DateTime> value) => value.Includes(Start) || value.Includes(End) || Includes(value);

		#region Static Members

		/// <summary>
		/// Open-started Date Range object ending at end date
		/// </summary>
		/// <param name="end">End date</param>
		/// <returns>DateRange object</returns>
		public static DateRange UpTo(DateTime end) => new DateRange(DateTime.MinValue, end);

		/// <summary>
		/// Open-ended Date Range beginning at start date
		/// </summary>
		/// <param name="start">Start date</param>
		/// <returns>DateRange object</returns>
		public static DateRange From(DateTime start) => new DateRange(start, DateTime.MaxValue);

		#endregion
	}
}
