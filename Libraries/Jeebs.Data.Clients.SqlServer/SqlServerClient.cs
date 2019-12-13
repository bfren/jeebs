﻿using System;
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
		IAdapter IDbClient.Adapter => Adapter;

		/// <summary>
		/// MySqlAdapter
		/// </summary>
		public SqlServerAdapter Adapter { get; }

		/// <summary>
		/// Create a MySqlConnection object using the specified connection string
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		/// <param name="encryptionKey">[Optional] Encryption key</param>
		/// <returns>IDbConnection (MySqlConnection) object</returns>
		public IDbConnection Connect(in string connectionString, in string? encryptionKey = null) => new SqlConnection(connectionString);

		/// <summary>
		/// Create object
		/// </summary>
		public SqlServerDbClient() => Adapter = new SqlServerAdapter();
	}
}
