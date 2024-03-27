// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

public static partial class DateTimeIntExtensions
{
	/// <summary>
	/// Deconstruct <paramref name="this"/> into values.
	/// </summary>
	/// <param name="this">DateTimeInt object.</param>
	/// <param name="year">Year.</param>
	/// <param name="month">Month.</param>
	/// <param name="day">Day.</param>
	/// <param name="hour">Hour.</param>
	/// <param name="minute">Minute.</param>
	public static void Deconstruct(this DateTimeInt @this, out int year, out int month, out int day, out int hour, out int minute) =>
		(year, month, day, hour, minute) = (@this.Year, @this.Month, @this.Day, @this.Hour, @this.Minute);
}
