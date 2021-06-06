// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.Config
{
	/// <summary>
	/// Database configuration
	/// </summary>
	public record DbConfig
	{
		/// <summary>
		/// Path to database settings configuration section
		/// </summary>
		public const string Key = JeebsConfig.Key + ":db";

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

			set =>
				authenticationConnectionValue = value;
		}

		private string? authenticationConnectionValue;

		/// <summary>
		/// Dictionary of database connections
		/// </summary>
		public Dictionary<string, DbConnectionConfig> Connections { get; init; } = new();

		/// <summary>
		/// Retrieve default Connection, unless name is set
		/// </summary>
		/// <param name="name">[Optional] Connection name</param>
		/// <exception cref="Jx.Config.DefaultDbConnectionUndefinedException"></exception>
		/// <exception cref="Jx.Config.NoDbConnectionsException"></exception>
		/// <exception cref="Jx.Config.NamedDbConnectionNotFoundException"></exception>
		public DbConnectionConfig GetConnection(string? name = null)
		{
			// If name is null or empty, use Default connection
			string connection = string.IsNullOrWhiteSpace(name) ? Default : name;
			if (string.IsNullOrEmpty(connection))
			{
				throw new Jx.Config.DefaultDbConnectionUndefinedException("Default database connection is not defined.");
			}

			// Attempt to retrieve the connection
			if (Connections.Count == 0)
			{
				throw new Jx.Config.NoDbConnectionsException("At least one database connection must be defined.");
			}

			if (Connections.TryGetValue(connection, out var config))
			{
				return config;
			}
			else
			{
				throw new Jx.Config.NamedDbConnectionNotFoundException(connection);
			}
		}

		/// <summary>
		/// Retrieve the authentication database connection settings
		/// </summary>
		public DbConnectionConfig GetAuthenticationConnection() =>
			GetConnection(Authentication);
	}
}
