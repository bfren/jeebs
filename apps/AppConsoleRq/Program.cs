// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppConsoleRq;
using Jeebs;
using Jeebs.Data;
using Jeebs.Data.Clients.Rqlite;
using Jeebs.Data.Enums;
using Microsoft.Extensions.DependencyInjection;
using RndF;
using Rqlite.Client;
using Wrap.Extensions;

var (app, log) = Jeebs.Apps.Host.Create(args, (ctx, services) =>
{
	services.AddRqlite();
	services.AddData<AppConsoleRq.Db, RqliteDbClient>();

	services.AddTransient<TestRepository>();
});

// Begin
static void header(string text, params object[] args)
{
	Console.WriteLine();
	Console.WriteLine(text, args);
	Console.WriteLine();
}
header("= Rqlite Console Test =");

// Get services
var db = app.Services.GetRequiredService<IDb>();
var dbTyped = app.Services.GetRequiredService<AppConsoleRq.Db>();
var http = app.Services.GetRequiredService<IHttpClientFactory>();
var client = http.CreateClient("with_timings");
var repo = app.Services.GetRequiredService<TestRepository>();

// Show rqlite version
using (var w = dbTyped.StartWork())
{
	var version = await w.GetVersionAsync();
	log.Inf("Rqlite: {Version}", version);
	log.Dbg("HTTP {Version}", client.DefaultRequestVersion);
}

// Create table
header("== Creating table ==");
const string table = "console";
await db.ExecuteAsync(
	$"CREATE TABLE IF NOT EXISTS {table} " +
	"(" +
	"id INTEGER NOT NULL PRIMARY KEY, " +
	"foo TEXT NOT NULL, " +
	"bar VARCHAR(6), " +
	"freddie INTEGER" +
	")",
	null
)
.LogBoolAsync(log);
Console.WriteLine();

// Insert into the table
header(" == Inserting data ==");
var foo = string.Empty;
var bar = string.Empty;
var freddie = 0;
for (var i = 0; i < 5; i++)
{
	(foo, bar, freddie) = (Rnd.Str, Rnd.Str, Rnd.Int);
	await db.ExecuteAsync(
		$"INSERT INTO {table} (foo, bar, freddie) VALUES (:foo, :bar, :freddie)",
		new { foo, bar, freddie }
	)
	.LogBoolAsync(log);
}

Console.WriteLine();

// Get data from the table
header("== Retrieving data ==");
await db.QueryAsync<TestObj>(
	$"SELECT * FROM {table}",
	null
)
.AuditAsync(
	fOk: x => x.Wrap().ToMaybe().Iterate(y => log.Dbg("{Item}", y)),
	fFail: log.Failure
);

var id = 0L;
await db.QuerySingleAsync<TestObj>(
	$"SELECT * FROM {table} WHERE foo = :foo AND bar = :bar", new { foo, bar }
)
.AuditAsync(
	fOk: x => { if (x.Foo == foo) { log.Dbg("Succeeded: {@Test}.", x); id = x.Id.Value; } else { log.Err("Failed."); } },
	fFail: log.Failure
);
Console.WriteLine();

// Update data
header("== Updating data ==");
var newFoo = Rnd.Str;
using (var w = dbTyped.StartWork())
{
	await w.ExecuteAsync(
		$"UPDATE {table} SET foo = :newFoo WHERE id = :id", new { newFoo, id }
	)
	.AuditAsync(
		fOk: x => { },
		fFail: log.Failure
	);

	await w.QueryAsync<TestObj>(
		$"SELECT * FROM {table} WHERE id = :id", new { id }
	)
	.GetSingleAsync(x => x.Value<TestObj>())
	.AuditAsync(
		fOk: x => { if (x.Foo == newFoo) { log.Dbg("Succeeded: {@Test}.", x); } else { log.Err("Failed."); } },
		fFail: log.Failure
	);
}

var newFoo2 = Rnd.Str;
await db.ExecuteAsync(
	$"UPDATE {table} SET foo = :newFoo2 WHERE id = :id", new { newFoo2, id }
)
.AuditAsync(
	fOk: x => { },
	fFail: log.Failure
);

await db.QueryAsync<TestObj>(
	$"SELECT * FROM {table} WHERE id = :id", new { id }
)
.GetSingleAsync(x => x.Value<TestObj>())
.AuditAsync(
	fOk: x => { if (x.Foo == newFoo2) { log.Dbg("Succeeded: {@Test}.", x); } else { log.Err("Failed."); } },
	fFail: log.Failure
);

Console.WriteLine();

// Return values using querying
header("== Retrieving values using query builder ==");
await db
	.QueryAsync<TestEntity>(
		builder => builder.From(dbTyped.Test).Where<TestTable>(t => t.Id, Compare.Equal, id)
	)
	.AuditAsync(
		fOk: x => log.Dbg("Found {Count} entities:", x.Count()),
		fFail: log.Failure
	)
	.IfOkAsync(
		x =>
		{
			foreach (var y in x)
			{
				log.Dbg("{@Value}", y);
			}
		}
	);

Console.WriteLine();

// Delete data
header("== Deleting data ==");
await db.ExecuteAsync(
	$"DELETE FROM {table} WHERE id = :id;", new { id }
)
.AuditAsync(
	fOk: x => { if (x) { log.Dbg("Succeeded."); } else { log.Err("Failed."); } },
	fFail: log.Failure
);
Console.WriteLine();

// Insert values using repository
header("== Inserting values using repository ==");
var v0 = new TestEntity(8, Rnd.Str, Rnd.Str, Rnd.UInt64);
var v1 = new TestEntity(19, Rnd.Str, Rnd.Str, Rnd.UInt64);
var v2 = new TestEntity(94, Rnd.Str, Rnd.Str, Rnd.UInt64);
var repoIds = new List<TestId>();
foreach (var v in new[] { v0, v1, v2 })
{
	await repo.CreateAsync(v)
	.AuditAsync(
		fOk: x => { log.Dbg("New ID: {Id}", x.Value); repoIds.Add(x); },
		fFail: log.Failure
	);
}
Console.WriteLine();

// Retrieving values using repository
header("== Retrieving values using repository ==");
foreach (var x in repoIds)
{
	await repo
		.RetrieveAsync<TestEntity>(x)
		.AuditAsync(
			fOk: x => log.Dbg("Found entity with ID: {Id} and Value: {@Value}", x.Id.Value, x),
			fFail: log.Failure
		);

	await repo.Fluent()
		.WhereId(x)
		.QuerySingleAsync<TestEntity>()
		.AuditAsync(
			fOk: x => log.Dbg("Found entity with ID: {Id} and Value: {@Value}", x.Id.Value, x),
			fFail: log.Failure
		);
}

await repo.Fluent()
	.WhereIn(x => x.Id, repoIds)
	.Sort(x => x.Id, SortOrder.Descending)
	.QueryAsync<TestEntity>()
	.AuditAsync(
		fOk: x => log.Dbg("Found: {@List}", x.ToList()),
		fFail: log.Failure
	);
Console.ReadLine();

// Deleting values using repository
header("== Deleting values using repository ==");
await repo
	.DeleteAsync(v0)
	.AuditAsync(
		fOk: x => log.Dbg("Deleted entity with ID: {Id} returned: {Value}", v0.Id.Value, x),
		fFail: log.Failure
	);
Console.ReadLine();

// Drop table
header("== Dropping table == ");
await db.ExecuteAsync(
	$"DROP TABLE {table}",
	null
)
.LogBoolAsync(log);
Console.WriteLine();
