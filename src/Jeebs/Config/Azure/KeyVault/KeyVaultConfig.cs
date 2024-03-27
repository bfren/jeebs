// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Microsoft.Extensions.Options;

namespace Jeebs.Config.Azure.KeyVault;

/// <summary>
/// Azure Key Vault Configuration.
/// </summary>
/// <remarks>
/// These values should only ever be set in jeebsconfig-secrets.json to avoid them being checked into version control
/// </remarks>
public sealed record class KeyVaultConfig : IOptions<KeyVaultConfig>
{
	/// <summary>
	/// Path to this configuration section.
	/// </summary>
	public static readonly string Key = AzureConfig.Key + ":keyVault";

	/// <summary>
	/// Azure Key Vault Name.
	/// </summary>
	public string Name { get; init; } = string.Empty;

	/// <summary>
	/// Tenant (or Directory) ID.
	/// </summary>
	public string TenantId { get; init; } = string.Empty;

	/// <summary>
	/// Client ID.
	/// </summary>
	public string ClientId { get; init; } = string.Empty;

	/// <summary>
	/// Client Secret.
	/// </summary>
	public string ClientSecret { get; init; } = string.Empty;

	/// <summary>
	/// Only returns True if <see cref="Name"/>, <see cref="TenantId"/>,
	/// <see cref="ClientId"/> and <see cref="ClientSecret"/> are all not null or empty.
	/// </summary>
	public bool IsValid =>
		!string.IsNullOrWhiteSpace(Name)
		&& !string.IsNullOrWhiteSpace(TenantId)
		&& !string.IsNullOrWhiteSpace(ClientId)
		&& !string.IsNullOrWhiteSpace(ClientSecret);

	/// <inheritdoc/>
	KeyVaultConfig IOptions<KeyVaultConfig>.Value =>
		this;

	/// <summary>
	/// Generate URI using <see cref="Name"/>.
	/// </summary>
	/// <returns>Azure Vault URI.</returns>
	public Uri GetUri() =>
		new($"https://{Name}.vault.azure.net/");
}
