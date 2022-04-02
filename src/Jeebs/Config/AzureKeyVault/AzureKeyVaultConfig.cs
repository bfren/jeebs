// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.AzureKeyVault;

/// <summary>
/// Azure Key Vault Configuration
/// These values should only ever be set in jeebsconfig-secrets.json to avoid them being checked into version control
/// </summary>
public sealed record class AzureKeyVaultConfig
{
	/// <summary>
	/// Path to this configuration section
	/// </summary>
	public static readonly string Key = JeebsConfig.Key + ":azureKeyVault";

	/// <summary>
	/// Azure Key Vault Name
	/// </summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// Tenant (or Directory) ID
	/// </summary>
	public string TenantId { get; init; } = string.Empty;

	/// <summary>
	/// Client ID
	/// </summary>
	public string ClientId { get; init; } = string.Empty;

	/// <summary>
	/// Client Secret
	/// </summary>
	public string ClientSecret { get; init; } = string.Empty;

	/// <summary>
	/// Only returns True if <see cref="Name"/>, <see cref="TenantId"/>, <see cref="ClientId"/> and <see cref="ClientSecret"/> are all not null or empty
	/// </summary>
	public bool IsValid =>
		!string.IsNullOrWhiteSpace(Name)
		&& !string.IsNullOrWhiteSpace(TenantId)
		&& !string.IsNullOrWhiteSpace(ClientId)
		&& !string.IsNullOrWhiteSpace(ClientSecret);
}
