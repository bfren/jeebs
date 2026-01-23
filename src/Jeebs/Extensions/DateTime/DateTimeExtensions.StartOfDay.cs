// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public static partial class DateTimeExtensions
{
	/// <summary>
	/// Return midnight on the specified day.
	/// </summary>
	/// <param name="this">DateTime object.</param>
	/// <returns>Midnight on <paramref name="this"/>.</returns>
	public static DateTime StartOfDay(this DateTime @this) =>
		new(@this.Year, @this.Month, @this.Day, 0, 0, 0, @this.Kind);
}
