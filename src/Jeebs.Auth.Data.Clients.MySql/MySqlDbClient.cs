// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using MySqlConnector;
using SimpleMigrations;
using SimpleMigrations.Console;
using SimpleMigrations.DatabaseProvider;

namespace Jeebs.Auth.Data.Clients.MySql;

/// <inheritdoc cref="IAuthDbClient"/>
public sealed class MySqlDbClient : Jeebs.Data.Clients.MySql.MySqlDbClient, IAuthDbClient
{
	private static void DoMigration(string connectionString, Action<SimpleMigrator> act)
	{
		// Connection to database
		using var db = new MySqlConnection(connectionString);

		// Get migration objects
		var provider = new MysqlDatabaseProvider(db) { TableName = $"`{AuthDb.Schema}.versioninfo`" };
		var migrator = new SimpleMigrator(typeof(MySqlDbClient).Assembly, provider, new ConsoleLogger());

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
