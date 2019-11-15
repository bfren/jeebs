using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Jeebs.Data.Clients.SqlServer
{
	/// <summary>
	/// SqlServer client
	/// </summary>
	public sealed class SqlServerDbClient : IDbClient
	{
		/// <summary>
		/// SqlServerAdapter
		/// </summary>
		public Lazy<IAdapter> Adapter { get; }

		/// <summary>
		/// Create a MySqlConnection object using the specified connection string
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		/// <param name="encryptionKey">[Optional] Encryption key</param>
		/// <returns>IDbConnection (MySqlConnection) object</returns>
		public IDbConnection Connect(in string connectionString, in string? encryptionKey = null) => new SqlConnection(connectionString);

		/// <summary>
		/// Setup object
		/// </summary>
		public SqlServerDbClient()
		{
			Adapter = new Lazy<IAdapter>(() => new SqlServerAdapter());
		}
	}
}
