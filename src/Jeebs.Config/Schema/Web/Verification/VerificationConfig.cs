// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config;

/// <summary>
/// Site Verification Configuration
/// </summary>
public sealed record class VerificationConfig
{
	/// <summary>
	/// Path to this configuration section
	/// </summary>
	public static readonly string Key = WebConfig.Key + ":verification";

	/// <summary>
	/// Google Site Verification page
	/// </summary>
	public string? Google
	{
		get =>
			F.StringF.Format("google{0}.html", googleCode);

		init =>
			googleCode = value;
	}

	private readonly string? googleCode;

	/// <summary>
	/// True if there are any verification configurations
	/// </summary>
	public bool Any =>
		Google is not null;
}
