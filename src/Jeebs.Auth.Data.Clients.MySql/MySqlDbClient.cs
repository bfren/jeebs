// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using Jeebs.Data.Common;
using Microsoft.Extensions.Logging;
using SimpleMigrations;
using SimpleMigrations.Console;
using SimpleMigrations.DatabaseProvider;

namespace Jeebs.Auth.Data.Clients.MySql;

/// <inheritdoc cref="IAuthDbClient"/>
public sealed class MySqlDbClient : Jeebs.Data.Clients.MySql.MySqlDbClient, IAuthDbClient
{
	/// <inheritdoc/>
	public MySqlDbClient(IAdapter adapter, ILoggerFactory loggerFactory) : base(adapter, loggerFactory) { }

	/// <inheritdoc/>
	public string GetUpdateUserLastSignInQuery() =>
		$@"
			UPDATE `{AuthDb.Schema}.{AuthUserTable.TableName}`
			SET `{new AuthUserTable().LastSignedIn}` = NOW()
			WHERE `{new AuthUserTable().Id}` = @id;
			SELECT ROW_COUNT();
		";

	private void DoMigration(string connectionString, Action<SimpleMigrator> act)
	{
		// Connection to database - from the shared data source, so MySqlConnector can log
		using var db = GetConnection(connectionString);

		// Get migration objects
		var provider = new MysqlDatabaseProvider(db) { TableName = $"`{AuthDb.Schema}.version_info`" };
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
