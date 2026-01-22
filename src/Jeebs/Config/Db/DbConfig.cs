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
		static Result<DbConnectionConfig> fail(string message, params object[] args) =>
			R.Fail(message, args).Ctx(nameof(DbConfig), nameof(GetConnection));

		// Name cannot be null or empty
		if (string.IsNullOrEmpty(name))
		{
			return fail("Default database connection is not defined.");
		}

		// The list of defined connections cannot be empty
		if (Connections.Count == 0)
		{
			return fail("At least one database connection must be defined.");
		}

		// Attempt to retrieve the connection
		if (Connections.TryGetValue(name, out var config))
		{
			return config;
		}

		return fail("A connection named {ConnectionName} could not be found.", name);
	}

	/// <summary>
	/// Retrieve the authentication database Connection defined by <see cref="Authentication"/>.
	/// </summary>
	/// <returns>The Authentication Connection configuration.</returns>
	public Result<DbConnectionConfig> GetAuthenticationConnection() =>
		GetConnection(Authentication);
}
