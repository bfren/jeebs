// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Globalization;
using Maybe;
using Maybe.Functions;
using Jeebs.Messages;

namespace Jeebs.Functions;

/// <summary>
/// DateTime functions
/// </summary>
public static class DateTimeF
{
	/// <summary>
	/// Convert a Unix timestamp to a DateTime object
	/// </summary>
	/// <param name="unixTimeStamp">Unix timestamp</param>
	public static DateTime FromUnix(double unixTimeStamp) =>
		new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTimeStamp).ToLocalTime();

	/// <summary>
	/// Returns a DateTime object representing the start of the Unix Epoch
	/// </summary>
	public static DateTime UnixEpoch() =>
		new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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

		return MaybeF.None<DateTime>(new M.InvalidDateTimeMsg(s));
	}

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Unable to parse DateTime string</summary>
		/// <param name="Value">DateTime string</param>
		public sealed record class InvalidDateTimeMsg(string Value) : WithValueMsg<string> { }
	}
}
