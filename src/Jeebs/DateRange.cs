// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs
{
	/// <inheritdoc cref="IRange{T}"/>
	public sealed class DateRange : IRange<DateTime>
	{
		/// <inheritdoc/>
		public DateTime Start { get; }

		/// <inheritdoc/>
		public DateTime End { get; }

		/// <inheritdoc/>
		public int Length =>
			End.Subtract(Start).Days + 1;

		/// <summary>
		/// Create range object from a single date
		/// </summary>
		/// <param name="single">Range start and end</param>
		public DateRange(DateTime single) =>
			(Start, End) = (single.StartOfDay(), single.EndOfDay());

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

		/// <inheritdoc/>
		public bool Includes(DateTime value) =>
			Start <= value && End >= value;

		/// <inheritdoc/>
		public bool Includes(IRange<DateTime> value) =>
			Start <= value.Start && End >= value.End;

		/// <inheritdoc/>
		public bool Overlaps(IRange<DateTime> value) =>
			value.Includes(Start) || value.Includes(End) || Includes(value);

		#region Static Members

		/// <summary>
		/// Open-started Date Range object ending at end date
		/// </summary>
		/// <param name="end">End date</param>
		/// <returns>DateRange object</returns>
		public static DateRange UpTo(DateTime end) =>
			new(DateTime.MinValue, end);

		/// <summary>
		/// Open-ended Date Range beginning at start date
		/// </summary>
		/// <param name="start">Start date</param>
		/// <returns>DateRange object</returns>
		public static DateRange From(DateTime start) =>
			new(start, DateTime.MaxValue);

		#endregion
	}
}
