// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Config;

/// <summary>
/// Jeebs Configuration
/// </summary>
public sealed record class JeebsConfig
{
	/// <summary>
	/// Path to Jeebs settings configuration section
	/// </summary>
	public static readonly string Key = "jeebs";

	/// <summary>
	/// App congiguration
	/// </summary>
	public App.AppConfig App { get; init; } = new();

	/// <summary>
	/// Azure configuration
	/// </summary>
	public Azure.AzureConfig Azure { get; init; } = new();

	/// <summary>
	/// Data configuration
	/// </summary>
	public Db.DbConfig Db { get; init; } = new();

	/// <summary>
	/// Logging congiguration
	/// </summary>
	public Logging.LoggingConfig Logging { get; init; } = new();

	/// <summary>
	/// Services configuration
	/// </summary>
	public Services.ServicesConfig Services { get; init; } = new();

	/// <summary>
	/// Web congiguration
	/// </summary>
	public Web.WebConfig Web { get; init; } = new();

	/// <summary>
	/// WordPress configurations
	/// </summary>
	public Dictionary<string, WordPress.WpConfig> Wp { get; init; } = new();

	/// <summary>
	/// If key starts with ':', add Jeebs config prefix
	/// </summary>
	/// <param name="key">Section key</param>
	public static string GetKey(string key) =>
		key.StartsWith(":", StringComparison.InvariantCulture) ? Key + key : key;
}
