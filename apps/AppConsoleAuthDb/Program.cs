// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Clients.MySql;
using Jeebs.Auth.Data.Clients.PostgreSql;
using Jeebs.Config.Db;
using Jeebs.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = Jeebs.Apps.Host.CreateBuilder(args);
var app = builder.Build();

var log = app.Services.GetRequiredService<ILog>();

// Begin
log.Dbg("= Auth Database Console Test =");

// Create clients
var postgres = new PostgreSqlDbClient();
var mariadb = new MySqlDbClient();

// Get config
var config = app.Services.GetRequiredService<IOptions<DbConfig>>().Value;

Console.WriteLine();

// PostgreSql
var postgresConn = config.GetConnection("server04-postgres").ConnectionString;

log.Dbg("== PostgreSQL ==");

log.Dbg("Nuke...");
postgres.Nuke(postgresConn);

log.Dbg("Migrate...");
postgres.MigrateToLatest(postgresConn);

Console.WriteLine();

// MariaDB
var mariadbConn = config.GetConnection("server04-mariadb").ConnectionString;

log.Dbg("== MariaDB ==");

log.Dbg("Nuke...");
mariadb.Nuke(mariadbConn);

log.Dbg("Migrate...");
mariadb.MigrateToLatest(mariadbConn);

Console.WriteLine();

// Done
Console.WriteLine("Done.");
Console.Read();
