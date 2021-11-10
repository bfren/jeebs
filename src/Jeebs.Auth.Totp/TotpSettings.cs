// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Auth.Totp
{
	/// <summary>
	/// TOTP settings
	/// </summary>
	public sealed record class TotpSettings
	{
		/// <summary>
		/// Default settings
		/// </summary>
		public static TotpSettings Default { get; } = new();

		/// <summary>
		/// The default interval period (in seconds)
		/// </summary>
		public int PeriodSeconds { get; init; } = 30;

		/// <summary>
		/// Enables tolerance either side of the current interval
		/// </summary>
		public bool IntervalTolerance { get; init; }

		/// <summary>
		/// The default length of verification code numbers
		/// </summary>
		public int CodeLength { get; init; } = 6;

		/// <summary>
		/// The default length of backup codes
		/// </summary>
		public int BackupCodeLength { get; init; } = 10;

		/// <summary>
		/// The default number of backup codes to generate
		/// </summary>
		public int NumberOfBackupCodes { get; init; } = 10;
	}
}
