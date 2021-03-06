// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Data;
using Jeebs.Config;
using Jeebs.Data.TypeHandlers;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDb"/>
	public abstract class Db : IDb
	{
		/// <summary>
		/// ILog
		/// </summary>
		protected ILog Log { get; }

		/// <summary>
		/// IDbClient
		/// </summary>
		protected IDbClient Client { get; }

		/// <summary>
		/// Connection String
		/// </summary>
		protected string ConnectionString { get; set; } = string.Empty;

		/// <inheritdoc/>
		public virtual IUnitOfWork UnitOfWork
		{
			get
			{
				// Make sure the connection string has been defined
				if (string.IsNullOrWhiteSpace(ConnectionString))
				{
					throw new Jx.Data.ConnectionException("You must define the connection string before creating a Unit of Work.");
				}

				// Connect to the database client
				var connection = Client.Connect(ConnectionString);
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				// Create Unit of Work
				return new UnitOfWork(connection, Client.Adapter, Log);
			}
		}

		/// <summary>
		/// Create object - you MUST set the connection string manually before calling <see cref="UnitOfWork"/>
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="log">ILog</param>
		protected Db(IDbClient client, ILog log) =>
			(Client, Log) = (client, log);

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="log">ILog</param>
		/// <param name="config">DbConfig</param>
		protected Db(IDbClient client, ILog log, DbConfig config) : this(client, log) =>
			ConnectionString = config.GetConnection().ConnectionString;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="log">ILog</param>
		/// <param name="config">DbConfig</param>
		/// <param name="connectionName">Connection name</param>
		protected Db(IDbClient client, ILog log, DbConfig config, string connectionName) : this(client, log) =>
			ConnectionString = config.GetConnection(connectionName).ConnectionString;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="log">ILog</param>
		/// <param name="connectionString">Connection String</param>
		protected Db(IDbClient client, ILog log, string connectionString) : this(client, log) =>
			ConnectionString = connectionString;

		#region Static

		/// <summary>
		/// Add default type handlers
		/// </summary>
		static Db() =>
			Dapper.SqlMapper.AddTypeHandler(new GuidTypeHandler());

		/// <summary>
		/// Persist an EnumList to the database by encoding it as JSON
		/// </summary>
		/// <typeparam name="T">Type to handle</typeparam>
		protected static void AddEnumeratedListTypeHandler<T>()
			where T : Enumerated =>
			Dapper.SqlMapper.AddTypeHandler(new EnumeratedListTypeHandler<T>());

		/// <summary>
		/// Persist a type to the database by encoding it as JSON
		/// </summary>
		/// <typeparam name="T">Type to handle</typeparam>
		protected static void AddJsonTypeHandler<T>() =>
			Dapper.SqlMapper.AddTypeHandler(new JsonTypeHandler<T>());

		#endregion
	}
}
