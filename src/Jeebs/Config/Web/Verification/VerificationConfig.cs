// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;
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
			StringF.Format("google{0}.html", googleCode, null);

		init =>
			googleCode = value;
	}

	private readonly string? googleCode;

	/// <summary>
	/// True if there are any verification configurations.
	/// </summary>
	public bool Any =>
		Google is not null;

	/// <inheritdoc/>
	VerificationConfig IOptions<VerificationConfig>.Value =>
		this;
}
