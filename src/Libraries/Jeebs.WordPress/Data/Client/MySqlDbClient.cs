// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
