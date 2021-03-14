// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using System.Data.SqlClient;

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
