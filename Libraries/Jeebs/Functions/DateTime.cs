using System;

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
		public static DateTime FromUnix(double unixTimeStamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTimeStamp).ToLocalTime();
		}

		/// <summary>
		/// Returns a DateTime object representing the start of the Unix Epoch
		/// </summary>
		/// <returns>Start of the Unix Epoch</returns>
		public static DateTime UnixEpoch()
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		}

		/// <summary>
		/// Create a DateTime object from a given format
		/// </summary>
		/// <param name="s">Input string</param>
		/// <param name="format">DateTime format</param>
		/// <param name="dt">Output DateTime</param>
		/// <returns>DateTime object, or null if the input string cannot be parsed</returns>
		public static bool FromFormat(string s, string format, out DateTime dt)
		{
			return DateTime.TryParseExact(
				s,
				format,
				System.Globalization.CultureInfo.InvariantCulture,
				System.Globalization.DateTimeStyles.None,
				out dt
			);
		}
	}
}
