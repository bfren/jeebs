// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Options;

namespace Jeebs.Config.Azure;

/// <summary>
/// Azure Configuration
/// </summary>
public sealed record class AzureConfig : IOptions<AzureConfig>
{
	/// <summary>
	/// Path to this configuration section
	/// </summary>
	public static readonly string Key = JeebsConfig.Key + ":azure";

	/// <summary>
	/// DataProtectionConfig
	/// </summary>
	public DataProtection.DataProtectionConfig Verification { get; init; } = new();

	/// <summary>
	/// KeyVaultConfig
	/// </summary>
	public KeyVault.KeyVaultConfig Redirections { get; init; } = new();

	/// <inheritdoc/>
	AzureConfig IOptions<AzureConfig>.Value =>
		this;
}
