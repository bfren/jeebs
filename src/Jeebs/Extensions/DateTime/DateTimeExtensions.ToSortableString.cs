// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs;

public static partial class DateTimeExtensions
{
	/// <summary>
	/// Return a sortable format date/time value (yyyy-MM-dd HH:mm:ss.ffff).
	/// </summary>
	/// <param name="this">DateTime object.</param>
	/// <returns>Sortable DateTime string.</returns>
	public static string ToSortableString(this DateTime @this) =>
		@this.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffff", CultureInfo.InvariantCulture);
}
