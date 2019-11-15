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
		/// Default database connection name
		/// </summary>
		public string Default { get; set; }

		/// <summary>
		/// Authentication database connection name
		/// </summary>
		public string Authentication
		{
			get => authentication ?? Default;
			set => authentication = value;
		}

		private string authentication;

		/// <summary>
		/// Dictionary of database connections
		/// </summary>
		public Dictionary<string, DbConnectionConfig> Connections { get; set; }

		/// <summary>
		/// Ensure validity
		/// </summary>
		/// <exception cref="Jx.ConfigException">If 'Default' connection is not defined, or there are no connections defined</exception>
		public DbConfig()
		{
			if (string.IsNullOrEmpty(Default))
			{
				throw new Jx.ConfigException($"{nameof(Default)} must be defined in DbConfig.");
			}

			if (Connections.Count == 0)
			{
				throw new Jx.ConfigException($"{nameof(Connections)} must contain at least one item.");
			}
		}

		/// <summary>
		/// Retrieve default Connection, unless name is set
		/// </summary>
		/// <param name="name">[Optional] Connection name</param>
		/// <exception cref="Jx.ConfigException">If a default name is not defined, or the requested connection was not found.</exception>
		/// <returns>Connection object</returns>
		public DbConnectionConfig GetConnection(string name = null)
		{
			// If name is null, use Default connection
			string connection = name ?? Default;

			// If both name and DefaultDbConnection are null, throw an exception
			if (string.IsNullOrEmpty(connection))
			{
				throw new Jx.ConfigException($"{nameof(Default)} is not defined in configuration settings.");
			}

			// Attempt to retrieve the connection
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
		/// <returns></returns>
		public DbConnectionConfig GetAuthenticationConnection() => GetConnection(Authentication);
	}
}
