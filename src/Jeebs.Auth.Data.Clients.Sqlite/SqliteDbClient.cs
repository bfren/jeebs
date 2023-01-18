// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data.SQLite;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;
using SimpleMigrations.Console;
using SimpleMigrations.DatabaseProvider;

namespace Jeebs.Auth.Data.Clients.Sqlite;

/// <inheritdoc cref="IAuthDbClient"/>
public sealed class SqliteDbClient : Jeebs.Data.Clients.Sqlite.SqliteDbClient, IAuthDbClient
{
	/// <inheritdoc/>
	public string GetUpdateUserLastSignInQuery() =>
		$@"
			UPDATE ""{AuthDb.Schema}.{AuthUserTable.TableName}""
			SET ""{new AuthUserTable().LastSignedIn}"" = datetime(""now"")
			WHERE ""{new AuthUserTable().Id}"" = @id
			RETURNING *;
		";

	private static void DoMigration(string connectionString, Action<SimpleMigrator> act)
	{
		// Connection to database
		using var db = new SQLiteConnection(connectionString);

		// Get migration objects
		var provider = new SqliteDatabaseProvider(db) { TableName = $"\"{AuthDb.Schema}.version_info\"" };
		var migrator = new SimpleMigrator(typeof(SqliteDbClient).Assembly, provider, new ConsoleLogger());

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
