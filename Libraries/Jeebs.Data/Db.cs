using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Jeebs.Data.TypeHandlers;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDb"/>
	/// <typeparam name="TDbClient">Database Client type</typeparam>
	public class Db<TDbClient> : IDb
		where TDbClient : IDbClient, new()
	{
		/// <summary>
		/// ILog
		/// </summary>
		protected readonly ILog log;

		/// <summary>
		/// TDbClient
		/// </summary>
		protected readonly TDbClient client = new TDbClient();

		/// <summary>
		/// Connection String
		/// </summary>
		protected string ConnectionString { get; set; } = string.Empty;

		/// <inheritdoc/>
		public IUnitOfWork UnitOfWork
		{
			get
			{
				// Make sure the connection string has been defined
				if (string.IsNullOrEmpty(ConnectionString))
				{
					throw new Jx.Data.ConnectionException("You must define the connection string before creating a Unit of Work.");
				}

				// Connect to the database client
				var connection = client.Connect(ConnectionString);
				if (connection.State != ConnectionState.Open)
				{
					connection.Open();
				}

				// Create Unit of Work
				return new UnitOfWork(connection, client.Adapter, log);
			}
		}

		/// <summary>
		/// Create object - you MUST set the connection string manually before calling <see cref="UnitOfWork"/>
		/// </summary>
		/// <param name="log">ILog</param>
		public Db(ILog log) => this.log = log;

		/// <summary>
		/// Create object using the specified connection string
		/// </summary>
		/// <param name="connectionString">Connection String</param>
		/// <param name="log">ILog</param>
		public Db(string connectionString, ILog log) : this(log) => ConnectionString = connectionString;

		/// <summary>
		/// Add default type handlers
		/// </summary>
		static Db() => Dapper.SqlMapper.AddTypeHandler(new GuidTypeHandler());

		/// <summary>
		/// Persist an EnumList to the database by encoding it as JSON
		/// </summary>
		/// <typeparam name="T">Type to handle</typeparam>
		protected static void AddEnumListTypeHandler<T>()
			where T : Enum
		{
			Dapper.SqlMapper.AddTypeHandler(new EnumListTypeHandler<T>());
		}

		/// <summary>
		/// Persist a type to the database by encoding it as JSON
		/// </summary>
		/// <typeparam name="T">Type to handle</typeparam>
		protected static void AddJsonTypeHandler<T>()
		{
			Dapper.SqlMapper.AddTypeHandler(new JsonTypeHandler<T>());
		}
	}
}
