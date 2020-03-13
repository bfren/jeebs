using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Database
	/// </summary>
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
		protected string ConnectionString { get; set; }

		/// <summary>
		/// Create a new UnitOfWork
		/// </summary>
		/// <exception cref="Jx.Data.ConnectionException">If the connection string has not yet been set</exception>
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
		public Db(ILog log)
		{
			ConnectionString = string.Empty;
			this.log = log;
		}

		/// <summary>
		/// Create object using the specified connection string
		/// </summary>
		/// <param name="connectionString">Connection String</param>
		/// <param name="log">ILog</param>
		public Db(string connectionString, ILog log) : this(log) => ConnectionString = connectionString;
	}
}
