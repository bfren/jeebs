// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using Jeebs.Config;
using Jeebs.WordPress.Data.TypeHandlers;

namespace Jeebs.WordPress.Data
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

		/// <summary>
		/// Log for Unit of Work
		/// </summary>
		private readonly ILog<UnitOfWork> unitOfWorkLog;

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
				return new UnitOfWork(connection, Client.Adapter, unitOfWorkLog);
			}
		}

		/// <summary>
		/// Create object - you MUST set the connection string manually before calling <see cref="UnitOfWork"/>
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="logs">DbLogs</param>
		protected Db(IDbClient client, DbLogs logs) =>
			(Client, Log, unitOfWorkLog) = (client, logs.DbLog, logs.UnitOfWorkLog);

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="logs">DbLogs</param>
		/// <param name="config">DbConfig</param>
		protected Db(IDbClient client, DbLogs logs, DbConfig config) : this(client, logs) =>
			ConnectionString = config.GetConnection().ConnectionString;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="logs">DbLogs</param>
		/// <param name="config">DbConfig</param>
		/// <param name="connectionName">Connection name</param>
		protected Db(IDbClient client, DbLogs logs, DbConfig config, string connectionName) : this(client, logs) =>
			ConnectionString = config.GetConnection(connectionName).ConnectionString;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="logs">DbLogs</param>
		/// <param name="connectionString">Connection String</param>
		protected Db(IDbClient client, DbLogs logs, string connectionString) : this(client, logs) =>
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
