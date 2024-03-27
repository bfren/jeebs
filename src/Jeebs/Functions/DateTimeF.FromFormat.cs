// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;

namespace Jeebs.Functions;

public static partial class DateTimeF
{
	/// <summary>
	/// Create a DateTime object from a given format.
	/// </summary>
	/// <param name="s">Input string.</param>
	/// <param name="format">DateTime format.</param>
	/// <returns>DateTime object.</returns>
	public static Maybe<DateTime> FromFormat(string s, string format)
	{
		if (DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
		{
			return dt;
		}

		return M.None;
	}
}
