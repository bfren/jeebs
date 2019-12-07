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
	public class Db<TDbClient> 
		where TDbClient : IDbClient, new()
	{
		/// <summary>
		/// IDbClient
		/// </summary>
		protected readonly TDbClient client = new TDbClient();

		/// <summary>
		/// TUnitOfWorkFactory object
		/// </summary>
		private readonly UnitOfWorkFactory unitOfWorkFactory = new UnitOfWorkFactory();

		/// <summary>
		/// Connection String
		/// </summary>
		protected string ConnectionString { get; set; }

		/// <summary>
		/// ILog
		/// </summary>
		protected ILog Log { get; }

		/// <summary>
		/// Create a new IUnitOfWork
		/// </summary>
		/// <exception cref="Jx.Data.ConnectionException">If the connection string has not yet been set</exception>
		public UnitOfWork UnitOfWork
		{
			get
			{
				if (string.IsNullOrEmpty(ConnectionString))
				{
					throw new Jx.Data.ConnectionException("You must define the connection string before creating a Unit of Work.");
				}

				var connection = client.Connect(ConnectionString);
				//if (connection.State != ConnectionState.Open)
				//{
				//	connection.Open();
				//}

				return unitOfWorkFactory.Create(connection, client.Adapter.Value, Log);
			}
		}

		/// <summary>
		/// Create object - you MUST set the connection string manually before calling <see cref="UnitOfWork"/>
		/// </summary>
		/// <param name="log">ILog</param>
		public Db(in ILog log)
		{
			ConnectionString = string.Empty;
			Log = log;
		}

		/// <summary>
		/// Create object using the specified connection string
		/// </summary>
		/// <param name="connectionString">Connection String</param>
		/// <param name="log">ILog</param>
		public Db(in string connectionString, in ILog log) : this(log) => ConnectionString = connectionString;
	}
}
