// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using MySql.Data.MySqlClient;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;

namespace Jeebs.Auth.Data.Clients.MySql
{
	/// <inheritdoc cref="IAuthDbClient"/>
	public sealed class MySqlDbClient : Jeebs.Data.Clients.MySql.MySqlDbClient, IAuthDbClient
	{
		/// <inheritdoc/>
		public void MigrateToLatest(string connectionString)
		{
			// Connection to database
			using var db = new MySqlConnection(connectionString);

			// Get migration objects
			var provider = new MysqlDatabaseProvider(db);
			var migrator = new SimpleMigrator(typeof(MySqlDbClient).Assembly, provider);

			// Get all the migrations
			migrator.Load();

			// Migrate to the latest version
			migrator.MigrateToLatest();
		}
	}
}
