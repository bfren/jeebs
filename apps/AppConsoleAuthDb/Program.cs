// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Clients.MySql;
using Jeebs.Auth.Data.Clients.PostgreSql;
using Jeebs.Auth.Data.Clients.Sqlite;
using Jeebs.Config.Db;
using Jeebs.Data.Adapters.Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var (app, log) = Jeebs.Apps.Host.Create(args);

// Begin
log.Dbg("= Auth Database Console Test =");

// Create clients
var adapter = DapperAdapter.DefaultInstance;
var postgres = new PostgreSqlDbClient();
var mariadb = new MySqlDbClient(adapter);
var sqlitedb = new SqliteDbClient(adapter);

// Get config
var config = app.Services.GetRequiredService<IOptions<DbConfig>>().Value;

Console.WriteLine();

// PostgreSql
var postgresConn = config.GetConnection("arwen-postgres").Unwrap().ConnectionString;

log.Dbg("== PostgreSQL ==");

log.Dbg("Nuke...");
postgres.Nuke(postgresConn);

log.Dbg("Migrate...");
postgres.MigrateToLatest(postgresConn);

Console.WriteLine();

// MariaDB
var mariadbConn = config.GetConnection("arwen-mariadb").Unwrap().ConnectionString;

log.Dbg("== MariaDB ==");

log.Dbg("Nuke...");
mariadb.Nuke(mariadbConn);

log.Dbg("Migrate...");
mariadb.MigrateToLatest(mariadbConn);

Console.WriteLine();

// Sqlite
var sqliteConn = config.GetConnection("local-sqlite").Unwrap().ConnectionString;

log.Dbg("== Sqlite ==");

log.Dbg("Nuke...");
sqlitedb.Nuke(sqliteConn);

log.Dbg("Migrate...");
sqlitedb.MigrateToLatest(sqliteConn);

Console.WriteLine();

// Done
Console.WriteLine("Done.");
Console.Read();
