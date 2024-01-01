// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs.Config.Db;

/// <summary>
/// Database connection configuration
/// </summary>
public sealed record class DbConnectionConfig
{
	/// <summary>
	/// Database connection string
	/// </summary>
	public string ConnectionString { get; init; } = string.Empty;

	/// <summary>
	/// Database table prefix
	/// </summary>
	public string TablePrefix { get; init; } = string.Empty;

	/// <summary>
	/// Additional settings required for configuring this database connection
	/// </summary>
	public Dictionary<string, string> AdditionalSettings { get; init; } = [];
}
