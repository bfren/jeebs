// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Options;

namespace Jeebs.Config.Web;

/// <summary>
/// Configuration options for Web Apps.
/// </summary>
public sealed record class WebConfig : IOptions<WebConfig>
{
	/// <summary>
	/// Path to this configuration section.
	/// </summary>
	public static readonly string Key = JeebsConfig.Key + ":web";

	/// <summary>
	/// Authentication and Authorisation configuration.
	/// </summary>
	public Auth.AuthConfig Auth { get; init; } = new();

	/// <summary>
	/// RedirectionsConfig.
	/// </summary>
	public Redirections.RedirectionsConfig Redirections { get; init; } = [];

	/// <summary>
	/// SiteVerificationConfig.
	/// </summary>
	public Verification.VerificationConfig Verification { get; init; } = new();

	/// <inheritdoc/>
	WebConfig IOptions<WebConfig>.Value =>
		this;
}
