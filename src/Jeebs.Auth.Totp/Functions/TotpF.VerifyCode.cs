// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Jeebs.Auth.Totp;

namespace F;

public static partial class TotpF
{
	/// <summary>
	/// Verify a code
	/// </summary>
	/// <param name="key">Secret key</param>
	/// <param name="code">Code to be verified</param>
	/// <param name="settings">TotpSettings</param>
	public static bool VerifyCode(byte[] key, string code, TotpSettings settings)
	{
		// Generate expected code and compare it
		var expected = GenerateCode(key, settings);
		if (expected == code)
		{
			return true;
		}
		else if (!settings.IntervalTolerance)
		{
			return false;
		}

		// Generate codes either side of current interval to compare
		var interval = GetCurrentInterval(settings.PeriodSeconds);
		var before = GenerateCode(key, interval - 1, settings.CodeLength);
		var after = GenerateCode(key, interval + 1, settings.CodeLength);
		return new[] { before, after }.Contains(code);
	}
}
