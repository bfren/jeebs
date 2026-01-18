// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Jeebs.Functions;

namespace Jeebs.Auth.Totp.Functions;

public static partial class TotpF
{
	/// <inheritdoc cref="VerifyCode(byte[], string, TotpSettings)"/>
	public static bool VerifyCode(string? key, string? code, TotpSettings settings) =>
		!string.IsNullOrEmpty(key)
		&& !string.IsNullOrEmpty(code)
		&& Base32F.FromBase32String(key).Switch(
			some: k => VerifyCode(k, code, settings),
			none: false
		);

	/// <summary>
	/// Verify a code.
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
