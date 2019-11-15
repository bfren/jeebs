using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace Jeebs.Data.Clients.MySql
{
	/// <summary>
	/// MySQL client
	/// </summary>
	public sealed class MySqlDbClient : IDbClient
	{
		/// <summary>
		/// MySqlAdapter
		/// </summary>
		public Lazy<IAdapter> Adapter { get; }

		/// <summary>
		/// Create a MySqlConnection object using the specified connection string
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		/// <param name="encryptionKey">[Optional] Encryption key</param>
		/// <returns>IDbConnection (MySqlConnection) object</returns>
		public IDbConnection Connect(in string connectionString, in string? encryptionKey = null) => new MySqlConnection(connectionString);

		/// <summary>
		/// Setup object
		/// </summary>
		public MySqlDbClient()
		{
			Adapter = new Lazy<IAdapter>(() => new MySqlAdapter());
		}
	}
}
