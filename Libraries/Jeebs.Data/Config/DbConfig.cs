using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Config
{
	/// <summary>
	/// Database configuration
	/// </summary>
	public sealed class DbConfig
	{
		/// <summary>
		/// Path to database settings configuration section
		/// </summary>
		public const string Key = ":db";

		/// <summary>
		/// Default database connection name
		/// </summary>
		public string Default { get; set; } = string.Empty;

		/// <summary>
		/// Authentication database connection name
		/// </summary>
		public string Authentication
		{
			get => authenticationConnectionValue ?? Default;
			set => authenticationConnectionValue = value;
		}

		private string? authenticationConnectionValue;

		/// <summary>
		/// Dictionary of database connections
		/// </summary>
		public Dictionary<string, DbConnectionConfig> Connections { get; set; } = new Dictionary<string, DbConnectionConfig>();

		/// <summary>
		/// Retrieve default Connection, unless name is set
		/// </summary>
		/// <param name="name">[Optional] Connection name</param>
		/// <exception cref="Jx.ConfigException">If a default name is not defined, or the requested connection was not found.</exception>
		public DbConnectionConfig GetConnection(string? name = null)
		{
			// If name is null, use Default connection
			string connection = name ?? Default;
			if (string.IsNullOrEmpty(connection))
			{
				throw new Jx.ConfigException($"{nameof(Default)} must be defined in DbConfig.");
			}

			// Attempt to retrieve the connection
			if (Connections.Count == 0)
			{
				throw new Jx.ConfigException($"{nameof(Connections)} must contain at least one item.");
			}

			if (Connections.TryGetValue(connection, out var config))
			{
				return config;
			}
			else
			{
				throw new Jx.ConfigException($"Connection '{connection}' was not found in configuration settings.");
			}
		}

		/// <summary>
		/// Retrieve the authentication database connection settings
		/// </summary>
		public DbConnectionConfig GetAuthenticationConnection() => GetConnection(Authentication);
	}
}
