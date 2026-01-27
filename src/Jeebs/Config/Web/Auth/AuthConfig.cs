// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Options;

namespace Jeebs.Config.Web.Auth;

/// <summary>
/// Jeebs Authentication and Authorisation configuration.
/// </summary>
public sealed record class AuthConfig : IOptions<AuthConfig>
{
	/// <summary>
	/// Path to this configuration section.
	/// </summary>
	public static readonly string Key = WebConfig.Key + ":auth";

	/// <summary>
	/// Whether or not auth is enabled.
	/// </summary>
	public bool Enabled { get; init; }

	/// <summary>
	/// Authentication scheme.
	/// </summary>
	public AuthScheme? Scheme { get; init; }

	/// <summary>
	/// Path to the login page.
	/// </summary>
	public string? LoginPath { get; init; }

	/// <summary>
	/// Path to the access denied page.
	/// </summary>
	public string? AccessDeniedPath { get; init; }

	/// <summary>
	/// JwtConfig.
	/// </summary>
	public Jwt.JwtConfig Jwt { get; init; } = new();

	/// <inheritdoc/>
	AuthConfig IOptions<AuthConfig>.Value =>
		this;
}
