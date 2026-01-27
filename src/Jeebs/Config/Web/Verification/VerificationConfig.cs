// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Options;

namespace Jeebs.Config.Web.Verification;

/// <summary>
/// Site Verification Configuration.
/// </summary>
public sealed record class VerificationConfig : IOptions<VerificationConfig>
{
	/// <summary>
	/// Path to this configuration section.
	/// </summary>
	public static readonly string Key = WebConfig.Key + ":verification";

	/// <summary>
	/// Google Site Verification page.
	/// </summary>
	public string? Google
	{
		get =>
			google switch
			{
				string =>
					string.Format(F.DefaultCulture, "google{0}.html", google),

				_ =>
					null
			};

		init =>
			google = value;
	}

	private string? google;

	/// <summary>
	/// True if there are any verification configurations.
	/// </summary>
	public bool Any =>
		!string.IsNullOrWhiteSpace(google);

	/// <inheritdoc/>
	VerificationConfig IOptions<VerificationConfig>.Value =>
		this;
}
