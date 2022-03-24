// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Npgsql;
using SimpleMigrations;
using SimpleMigrations.Console;
using SimpleMigrations.DatabaseProvider;

namespace Jeebs.Auth.Data.Clients.PostgreSql;

/// <inheritdoc cref="IAuthDbClient"/>
public sealed class PostgreSqlDbClient : Jeebs.Data.Clients.PostgreSql.PostgreSqlDbClient, IAuthDbClient
{
	private static void DoMigration(string connectionString, Action<SimpleMigrator> act)
	{
		// Connection to database
		using var db = new NpgsqlConnection(connectionString);

		// Get migration objects
		var provider = new PostgresqlDatabaseProvider(db);
		var migrator = new SimpleMigrator(typeof(PostgreSqlDbClient).Assembly, provider, new ConsoleLogger());

		// Get all the migrations
		migrator.Load();

		// Perform the migration
		act(migrator);
	}

	/// <inheritdoc/>
	public void Nuke(string connectionString) =>
		DoMigration(connectionString, migrator => migrator.MigrateTo(0));

	/// <inheritdoc/>
	public void MigrateToLatest(string connectionString) =>
		DoMigration(connectionString, migrator => migrator.MigrateToLatest());
}
