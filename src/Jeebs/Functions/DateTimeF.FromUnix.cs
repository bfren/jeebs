// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Functions;

public static partial class DateTimeF
{
	/// <summary>
	/// Convert a Unix timestamp to a DateTime object
	/// </summary>
	/// <param name="unixTimeStamp">Unix timestamp</param>
	public static DateTime FromUnix(double unixTimeStamp) =>
		new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTimeStamp).ToLocalTime();
}
