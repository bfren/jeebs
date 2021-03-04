using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Jeebs.Data.Clients.SqlServer
{
	/// <inheritdoc cref="IDbClient"/>
	public sealed class SqlServerDbClient : IDbClient
	{
		/// <inheritdoc/>
		IAdapter IDbClient.Adapter =>
			Adapter;

		/// <summary>
		/// MySqlAdapter
		/// </summary>
		public SqlServerAdapter Adapter { get; } = new();

		/// <inheritdoc/>
		public IDbConnection Connect(string connectionString, string? encryptionKey = null) =>
			new SqlConnection(connectionString);
	}
}
