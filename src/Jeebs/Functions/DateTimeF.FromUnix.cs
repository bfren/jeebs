// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Functions;

public static partial class DateTimeF
{
	/// <summary>
	/// Convert a Unix timestamp to a DateTime object.
	/// </summary>
	/// <param name="unixTimeStampInSeconds">Unix timestamp (in seconds).</param>
	/// <returns>DateTime object.</returns>
	public static DateTime FromUnix(double unixTimeStampInSeconds) =>
		DateTime.UnixEpoch.AddSeconds(unixTimeStampInSeconds).ToUniversalTime();
}
