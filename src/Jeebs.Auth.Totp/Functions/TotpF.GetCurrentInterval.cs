// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Auth.Totp.Functions;

public static partial class TotpF
{
	/// <summary>
	/// Return the current interval, calculated from the Unix Epoch.
	/// </summary>
	/// <param name="periodSeconds">The number of seconds per interval.</param>
	public static ulong GetCurrentInterval(int periodSeconds)
	{
		var elapsedSeconds = (ulong)Math.Floor((DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds);
		return elapsedSeconds / (ulong)periodSeconds;
	}
}
