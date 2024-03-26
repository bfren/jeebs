// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Functions;

public static partial class DateTimeF
{
	/// <summary>
	/// Returns a DateTime object representing the start of the Unix Epoch.
	/// </summary>
	public static DateTime UnixEpoch() =>
		new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}
