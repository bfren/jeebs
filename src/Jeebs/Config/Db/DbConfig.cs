// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Jeebs.Config.Db;

/// <summary>
/// Database configuration.
/// </summary>
public sealed record class DbConfig : IOptions<DbConfig>
{
	/// <summary>
	/// Path to database settings configuration section.
	/// </summary>
	public static readonly string Key = JeebsConfig.Key + ":db";

	/// <summary>
	/// Default database connection name.
	/// </summary>
	public string Default { get; init; } = string.Empty;

	/// <summary>
	/// Authentication database connection name.
	/// </summary>
	public string Authentication
	{
		get =>
			field ?? Default;

		init;
	}

	/// <summary>
	/// Dictionary of database connections.
	/// </summary>
	public Dictionary<string, DbConnectionConfig> Connections { get; init; } = [];

	/// <inheritdoc/>
	DbConfig IOptions<DbConfig>.Value =>
		this;

	/// <summary>
	/// Retrieve default Connection.
	/// </summary>
	public Result<DbConnectionConfig> GetConnection() =>
		GetConnection(Default);

	/// <summary>
	/// Retrieve Connection by name.
	/// </summary>
	/// <param name="name">Connection name.</param>
	public Result<DbConnectionConfig> GetConnection(string name)
	{
		static Result<DbConnectionConfig> fail(string message, params object?[] args) =>
			R.Fail(message, args).Ctx(nameof(DbConfig), nameof(GetConnection));

		// Name cannot be null or empty
		var nameOrDefault = string.IsNullOrWhiteSpace(name) ? Default : name;
		if (string.IsNullOrWhiteSpace(nameOrDefault))
		{
			return fail("You must specify the name of the default database connection.");
		}

		// The list of defined connections cannot be empty
		if (Connections.Count == 0)
		{
			return fail("At least one database connection must be defined.");
		}

		// Attempt to retrieve the connection
		if (Connections.TryGetValue(nameOrDefault, out var config))
		{
			return config;
		}

		return fail("A connection named '{Name}' could not be found.", nameOrDefault);
	}

	/// <summary>
	/// Retrieve the authentication database Connection defined by <see cref="Authentication"/>.
	/// </summary>
	/// <returns>The Authentication Connection configuration.</returns>
	public Result<DbConnectionConfig> GetAuthenticationConnection() =>
		GetConnection(Authentication);
}
