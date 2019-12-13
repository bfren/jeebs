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
		/// IAdapter
		/// </summary>
		IAdapter IDbClient.Adapter => Adapter;

		/// <summary>
		/// MySqlAdapter
		/// </summary>
		public MySqlAdapter Adapter { get; }

		/// <summary>
		/// Create a MySqlConnection object using the specified connection string
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		/// <param name="encryptionKey">[Optional] Encryption key</param>
		/// <returns>IDbConnection (MySqlConnection) object</returns>
		public IDbConnection Connect(in string connectionString, in string? encryptionKey = null) => new MySqlConnection(connectionString);

		/// <summary>
		/// Create object
		/// </summary>
		public MySqlDbClient() => Adapter = new MySqlAdapter();
	}
}
