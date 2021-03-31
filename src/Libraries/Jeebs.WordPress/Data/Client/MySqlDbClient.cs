// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using MySql.Data.MySqlClient;

namespace Jeebs.WordPress.Data.Clients.MySql
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
		public MySqlAdapter Adapter { get; } = new();

		/// <inheritdoc/>
		public IDbConnection Connect(string connectionString, string? encryptionKey = null) =>
			new MySqlConnection(connectionString);
	}
}
