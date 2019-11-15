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
	public sealed class Db<TDbClient> where TDbClient : IDbClient, new()
	{
		/// <summary>
		/// IDbClient
		/// </summary>
		public TDbClient Client { get; }

		/// <summary>
		/// Connection String
		/// </summary>
		private readonly string connectionString;

		/// <summary>
		/// ILog
		/// </summary>
		private readonly ILog log;

		/// <summary>
		/// TUnitOfWorkFactory object
		/// </summary>
		private readonly UnitOfWorkFactory unitOfWorkFactory;

		/// <summary>
		/// Create a new IUnitOfWork
		/// </summary>
		/// <exception cref="Jx.Data.ConnectionException">If the connection string has not yet been set</exception>
		public UnitOfWork UnitOfWork
		{
			get
			{
				if (string.IsNullOrEmpty(connectionString))
				{
					throw new Jx.Data.ConnectionException("You must define the connection string before creating a Unit of Work.");
				}

				var connection = Client.Connect(connectionString);
				//if (connection.State != ConnectionState.Open)
				//{
				//	connection.Open();
				//}

				return unitOfWorkFactory.Create(connection, log);
			}
		}

		/// <summary>
		/// Create object using the specified connection string
		/// </summary>
		/// <param name="connectionString">Connection String</param>
		/// <param name="log">ILog</param>
		public Db(in string connectionString, in ILog log)
		{
			Client = new TDbClient();
			this.connectionString = connectionString;
			this.log = log;
			unitOfWorkFactory = new UnitOfWorkFactory();
		}
	}
}
