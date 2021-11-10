// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppConsoleAuthDb;
using Jeebs.Auth.Data.Clients.MySql;
using Jeebs.Auth.Data.Clients.PostgreSql;
using Jeebs.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

await Jeebs.Apps.Program.MainAsync<App>(args, async (provider, log) =>
{
	// Begin
	log.Debug("= Auth Database Console Test =");

	// Create clients
	var postgres = new PostgreSqlDbClient();
	var mariadb = new MySqlDbClient();

	// Get config
	var config = provider.GetRequiredService<IOptions<DbConfig>>().Value;

	Console.WriteLine();

	// PostgreSql
	var postgresConn = config.GetConnection("server04-postgres").ConnectionString;

	log.Debug("== PostgreSQL ==");

	log.Debug("Nuke...");
	postgres.Nuke(postgresConn);

	log.Debug("Migrate...");
	postgres.MigrateToLatest(postgresConn);

	Console.WriteLine();

	// MariaDB
	var mariadbConn = config.GetConnection("server04-mariadb").ConnectionString;

	log.Debug("== MariaDB ==");

	log.Debug("Nuke...");
	mariadb.Nuke(mariadbConn);

	log.Debug("Migrate...");
	mariadb.MigrateToLatest(mariadbConn);

	Console.WriteLine();

	// Done
	Console.WriteLine("Done.");
	Console.Read();
});
