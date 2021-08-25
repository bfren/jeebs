// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Globalization;
using Jeebs;
using static F.OptionF;

namespace F
{
	/// <summary>
	/// DateTime functions
	/// </summary>
	public static class DateTimeF
	{
		/// <summary>
		/// Convert a Unix timestamp to a DateTime object
		/// </summary>
		/// <param name="unixTimeStamp">Unix timestamp</param>
		/// <returns>Returns DateTime object</returns>
		public static DateTime FromUnix(double unixTimeStamp) =>
			new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTimeStamp).ToLocalTime();

		/// <summary>
		/// Returns a DateTime object representing the start of the Unix Epoch
		/// </summary>
		/// <returns>Start of the Unix Epoch</returns>
		public static DateTime UnixEpoch() =>
			new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// Create a DateTime object from a given format
		/// </summary>
		/// <param name="s">Input string</param>
		/// <param name="format">DateTime format</param>
		/// <returns>DateTime object, or null if the input string cannot be parsed</returns>
		public static Option<DateTime> FromFormat(string s, string format)
		{
			if (DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
			{
				return dt;
			}

			return None<DateTime>(new Msg.InvalidDateTimeMsg(s));
		}

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Unable to parse DateTime string</summary>
			/// <param name="Value">DateTime string</param>
			public sealed record class InvalidDateTimeMsg(string Value) : WithValueMsg<string> { }
		}
	}
}
