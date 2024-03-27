// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Microsoft.Extensions.Options;

namespace Jeebs.Config.Azure.DataProtection;

/// <summary>
/// Azure Key Vault Configuration.
/// </summary>
/// <remarks>
/// These values should only ever be set in jeebsconfig-secrets.json to avoid them being checked into version control
/// </remarks>
public sealed record class DataProtectionConfig : IOptions<DataProtectionConfig>
{
	/// <summary>
	/// Path to this configuration section.
	/// </summary>
	public static readonly string Key = AzureConfig.Key + ":dataProtection";

	/// <summary>
	/// Azure storage access key connection string.
	/// </summary>
	public string StorageAccessKeyConnectionString { get; init; } = string.Empty;

	/// <summary>
	/// Blob Container name.
	/// </summary>
	public string ContainerName { get; init; } = string.Empty;

	/// <summary>
	/// Blob name (e.g. keys.xml).
	/// </summary>
	public string BlobName { get; init; } = "keys.xml";

	/// <summary>
	/// URI to encryption key in Azure Key Vault.
	/// </summary>
	public string KeyUri { get; init; } = string.Empty;

	/// <summary>
	/// Only returns True if <see cref="StorageAccessKeyConnectionString"/>,
	/// <see cref="ContainerName"/>, <see cref="BlobName"/>,
	/// and <see cref="KeyUri"/> are all not null or empty.
	/// </summary>
	public bool IsValid =>
		!string.IsNullOrWhiteSpace(StorageAccessKeyConnectionString)
		&& !string.IsNullOrWhiteSpace(ContainerName)
		&& !string.IsNullOrWhiteSpace(BlobName)
		&& !string.IsNullOrWhiteSpace(KeyUri);

	/// <inheritdoc/>
	DataProtectionConfig IOptions<DataProtectionConfig>.Value =>
		this;

	/// <summary>
	/// Get Azure Key URI.
	/// </summary>
	public Uri GetUri() =>
		new(KeyUri);
}
