// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Npgsql;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;

namespace Jeebs.Auth.Data.Clients.PostgreSql;

/// <inheritdoc cref="IAuthDbClient"/>
public sealed class PostgreSqlDbClient : Jeebs.Data.Clients.PostgreSql.PostgreSqlDbClient, IAuthDbClient
{
	/// <inheritdoc/>
	public void MigrateToLatest(string connectionString)
	{
		// Connection to database
		using var db = new NpgsqlConnection(connectionString);

		// Get migration objects
		var provider = new PostgresqlDatabaseProvider(db);
		var migrator = new SimpleMigrator(typeof(PostgreSqlDbClient).Assembly, provider);

		// Get all the migrations
		migrator.Load();

		// Migrate to the latest version
		migrator.MigrateToLatest();
	}
}
