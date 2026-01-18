// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs.Data;

/// <summary>
/// <see cref="DateTime"/> Extensions.
/// </summary>
public static class DateTimeExtensions
{
	/// <summary>
	/// Return a MySql formatted DateTime string for the specified date (yyyy-MM-dd HH:mm:ss).
	/// </summary>
	/// <param name="this">DateTime object</param>
	public static string ToMySqlString(this DateTime @this) =>
		@this.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
}
