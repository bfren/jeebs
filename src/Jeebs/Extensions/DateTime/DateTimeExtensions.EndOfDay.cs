// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public static partial class DateTimeExtensions
{
	/// <summary>
	/// Return one second to midnight on the specified day.
	/// </summary>
	/// <param name="this">DateTime object.</param>
	/// <returns>One second to midnight on <paramref name="this"/>.</returns>
	public static DateTime EndOfDay(this DateTime @this) =>
		new(@this.Year, @this.Month, @this.Day, 23, 59, 59, @this.Kind);
}
