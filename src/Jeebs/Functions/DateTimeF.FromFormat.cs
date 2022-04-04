// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;
using Jeebs.Messages;

namespace Jeebs.Functions;

public static partial class DateTimeF
{
	/// <summary>
	/// Create a DateTime object from a given format
	/// </summary>
	/// <param name="s">Input string</param>
	/// <param name="format">DateTime format</param>
	public static Maybe<DateTime> FromFormat(string s, string format)
	{
		if (DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
		{
			return dt;
		}

		return F.None<DateTime>(new M.InvalidDateTimeMsg(s));
	}

	public static partial class M
	{
		/// <summary>Unable to parse DateTime string</summary>
		/// <param name="Value">DateTime string</param>
		public sealed record class InvalidDateTimeMsg(string Value): WithValueMsg<string>;
	}
}
