using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace Jeebs.Data.Clients.MySql
{
	/// <inheritdoc cref="IDbClient"/>
	public sealed class MySqlDbClient : IDbClient
	{
		/// <inheritdoc/>
		IAdapter IDbClient.Adapter =>
			Adapter;

		/// <summary>
		/// MySqlAdapter
		/// </summary>
		public MySqlAdapter Adapter { get; } = new MySqlAdapter();

		/// <inheritdoc/>
		public IDbConnection Connect(string connectionString, string? encryptionKey = null) =>
			new MySqlConnection(connectionString);
	}
}
