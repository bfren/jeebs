// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.Options;

namespace Jeebs.Config.WordPress;

/// <summary>
/// WordPress configuration
/// </summary>
public record class WpConfig : IOptions<WpConfig>
{
	/// <summary>
	/// Path to WordPress settings configuration section
	/// </summary>
	public static readonly string Key = JeebsConfig.Key + ":wp";

	/// <summary>
	/// Database connection name (accessed via main Jeebs DB settings section)
	/// If empty, will attempt to use the default connection (as defined in main Jeebs DB settings section)
	/// </summary>
	public string Db { get; init; } = string.Empty;

	/// <summary>
	/// Overrides the table prefix in the DB connection settings
	/// </summary>
	public string TablePrefix { get; init; } = string.Empty;

	/// <summary>
	/// /wp-content/uploads URL (for replacing with local URL)
	/// </summary>
	public string UploadsUrl { get; init; } = string.Empty;

	/// <summary>
	/// /wp-content/uploads path (to access files directly)
	/// </summary>
	public string UploadsPath { get; init; } = string.Empty;

	/// <summary>
	/// Files URL (alias to hide wp-content/uploads directory)
	/// </summary>
	public string VirtualUploadsUrl { get; init; } = string.Empty;

	/// <inheritdoc/>
	WpConfig IOptions<WpConfig>.Value =>
		this;
}
