// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Data
{
	/// <summary>
	/// DateTime Extensions
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Return a MySql formatted DateTime string for the specified date (yyyy-MM-dd HH:mm:ss)
		/// </summary>
		/// <param name="this">DateTime object</param>
		/// <returns>MySql Formatted string</returns>
		public static string ToMySqlString(this DateTime @this) =>
			@this.ToString("yyyy-MM-dd HH:mm:ss");
	}
}
