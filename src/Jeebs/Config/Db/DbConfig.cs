// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Jeebs.Config.Db;

/// <summary>
/// Database configuration
/// </summary>
public sealed record class DbConfig : IOptions<DbConfig>
{
	/// <summary>
	/// Path to database settings configuration section
	/// </summary>
	public static readonly string Key = JeebsConfig.Key + ":db";

	/// <summary>
	/// Default database connection name
	/// </summary>
	public string Default { get; init; } = string.Empty;

	/// <summary>
	/// Authentication database connection name
	/// </summary>
	public string Authentication
	{
		get =>
			authenticationConnectionValue ?? Default;

		init =>
			authenticationConnectionValue = value;
	}

	private readonly string? authenticationConnectionValue;

	/// <summary>
	/// Dictionary of database connections
	/// </summary>
	public Dictionary<string, DbConnectionConfig> Connections { get; init; } = [];

	/// <inheritdoc/>
	DbConfig IOptions<DbConfig>.Value =>
		this;

	/// <summary>
	/// Retrieve default Connection
	/// </summary>
	/// <exception cref="DefaultDbConnectionUndefinedException"></exception>
	/// <exception cref="NoDbConnectionsException"></exception>
	/// <exception cref="NamedDbConnectionNotFoundException"></exception>
	public DbConnectionConfig GetConnection() =>
		GetConnection(null);

	/// <summary>
	/// Retrieve Connection by name
	/// </summary>
	/// <param name="name">[Optional] Connection name</param>
	/// <exception cref="DefaultDbConnectionUndefinedException"></exception>
	/// <exception cref="NoDbConnectionsException"></exception>
	/// <exception cref="NamedDbConnectionNotFoundException"></exception>
	public DbConnectionConfig GetConnection(string? name)
	{
		// If name is null or empty, use Default connection
		var connection = string.IsNullOrWhiteSpace(name) ? Default : name;
		if (string.IsNullOrEmpty(connection))
		{
			throw new DefaultDbConnectionUndefinedException("Default database connection is not defined.");
		}

		// Attempt to retrieve the connection
		if (Connections.Count == 0)
		{
			throw new NoDbConnectionsException("At least one database connection must be defined.");
		}

		if (Connections.TryGetValue(connection, out var config))
		{
			return config;
		}

		throw new NamedDbConnectionNotFoundException(connection);
	}

	/// <summary>
	/// Retrieve the authentication database connection settings
	/// </summary>
	public DbConnectionConfig GetAuthenticationConnection() =>
		GetConnection(Authentication);
}
