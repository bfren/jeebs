// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using AppConsolePg;
using Jeebs;
using Jeebs.Data.Clients.PostgreSql.Parameters;
using Jeebs.Data.Clients.PostgreSql.TypeHandlers;
using Jeebs.Data.Enums;
using Microsoft.Extensions.DependencyInjection;
using RndF;
using Wrap.Extensions;

var (app, log) = Jeebs.Apps.Host.Create<App>(args);

// Begin
log.Inf("= PostgreSQL Console Test =");

// Get services
var db = app.Services.GetRequiredService<Db>();
var repo = app.Services.GetRequiredService<JsonRepository>();
Console.WriteLine();

// Create schema
log.Dbg("== Creating schema ==");
const string schema = "console";
await db
	.ExecuteAsync(
		$"CREATE SCHEMA IF NOT EXISTS {schema};",
		null,
		CommandType.Text
	);
Console.WriteLine();

// Create table
log.Dbg("== Creating table ==");
const string table = $"{schema}.test";
await db
	.ExecuteAsync(
		$"CREATE TABLE IF NOT EXISTS {table} " +
		"(" +
		"id integer NOT NULL GENERATED ALWAYS AS IDENTITY, " +
		"foo text NOT NULL, " +
		"bar character(6), " +
		"freddie integer" +
		");",
		null,
		CommandType.Text
	);
Console.WriteLine();

// Insert into the table
log.Dbg(" == Inserting data ==");
var foo = Rnd.Str;
var bar = Rnd.Str;
await db
	.ExecuteAsync(
		$"INSERT INTO {table} (foo, bar) VALUES (@foo, @bar);", new { foo, bar }, CommandType.Text
	)
	.AuditAsync(
		fail: log.Failure
	);
Console.WriteLine();

// Get data from the table
log.Dbg("== Retrieving data ==");
var id = 0L;
await db
	.QuerySingleAsync<TestObj>(
		$"SELECT * FROM {table} WHERE foo = @foo AND bar = @bar;", new { foo, bar }, CommandType.Text
	)
	.AuditAsync(
		ok: x => { if (x.Foo == foo) { log.Dbg("Succeeded: {@Test}.", x); id = x.Id.Value; } else { log.Err("Failed."); } },
		fail: log.Failure
	);
Console.WriteLine();

// Update data
log.Dbg("== Updating data ==");
var newFoo = Rnd.Str;
await db
	.ExecuteAsync(
		$"UPDATE {table} SET foo = @newFoo WHERE id = @id;", new { newFoo, id }, CommandType.Text
	)
	.AuditAsync(
		fail: log.Failure
	);

await db
	.QuerySingleAsync<TestObj>(
		$"SELECT * FROM {table} WHERE id = @id;", new { id }, CommandType.Text
	)
	.AuditAsync(
		ok: x => { if (x.Foo == newFoo) { log.Dbg("Succeeded: {@Test}.", x); } else { log.Err("Failed."); } },
		fail: log.Failure
	);
Console.WriteLine();

// Return values using querying
log.Dbg("== Retrieving values using querying ==");
using (var w = await db.StartWorkAsync())
{
	var queryMatch = await db
		.QueryAsync<TestObj>(
			builder => builder.From(db.Test).Where<TestTable>(t => t.Id, Compare.Equal, id)
		)
		.AuditAsync(
			ok: x => log.Dbg("Found {Count} entities:", x.Count()),
			fail: log.Failure
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
}

Console.ReadLine();

// Delete data
log.Dbg("== Deleting data ==");
await db
	.ExecuteAsync(
		$"DELETE FROM {table} WHERE id = @id;", new { id }, CommandType.Text
	)
	.AuditAsync(
		ok: x => { if (x) { log.Dbg("Succeeded."); } else { log.Err("Failed."); } },
		fail: log.Failure
	);
Console.WriteLine();

// Drop table
log.Dbg("== Dropping table == ");
await db
	.ExecuteAsync(
		$"DROP TABLE {table};", null, CommandType.Text
	);
Console.WriteLine();

// Create table
log.Dbg("== Creating table with JSON ==");
const string jsonTable = "console.test_json";
await db
	.ExecuteAsync(
		$"CREATE TABLE IF NOT EXISTS {jsonTable} " +
		"(" +
		"\"json_id\" integer NOT NULL GENERATED ALWAYS AS IDENTITY, " +
		"\"json_value\" jsonb NOT NULL" +
		");",
		null,
		CommandType.Text
	);
Console.WriteLine();

// Insert values using Jsonb
log.Dbg("== Inserting values as Jsonb ==");
var v0 = new TestObj(7, Rnd.Str, Rnd.Str, Rnd.UInt64);
var v1 = new TestObj(18, Rnd.Str, Rnd.Str, Rnd.UInt64);
var v2 = new TestObj(93, Rnd.Str, Rnd.Str, Rnd.UInt64);
using (var w = await db.StartWorkAsync())
{
	foreach (var v in new[] { v0, v1, v2 })
	{
		await db
			.ExecuteAsync(
				$"INSERT INTO {jsonTable} (json_value) VALUES (@value);", new { value = Jsonb.Create(v) }, CommandType.Text, w.Transaction
			)
			.AuditAsync(
				fail: log.Failure
			);
	}
}
Console.WriteLine();

log.Dbg("== Checking Jsonb insert has worked ==");
await db
	.QuerySingleAsync<long>(
		$"SELECT json_value -> '{nameof(TestObj.Id).ToCamelCase()}' FROM {jsonTable} WHERE json_value ->> '{nameof(TestObj.Foo).ToCamelCase()}' = @foo;",
		new { foo = v1.Foo },
		CommandType.Text
	)
	.AuditAsync(
		ok: x => { if (x == 18) { log.Dbg("Succeeded: {@Test}.", x); } else { log.Err("Failed: {@Test}.", x); } },
		fail: log.Failure
	);
Console.WriteLine();

Console.ReadLine();

// Insert values using repository
log.Dbg("== Inserting values using repository ==");
Dapper.SqlMapper.AddTypeHandler(new JsonbTypeHandler<TestObj>()); // add here so doesn't interfere with earlier tests
var v3 = new TestObj(8, Rnd.Str, Rnd.Str, Rnd.UInt64);
var v4 = new TestObj(19, Rnd.Str, Rnd.Str, Rnd.UInt64);
var v5 = new TestObj(94, Rnd.Str, Rnd.Str, Rnd.UInt64);
var repoIds = new List<TestId>();
using (var w = await db.StartWorkAsync())
{
	foreach (var v in new[] { v3, v4, v5 })
	{
		await repo
			.CreateAsync(
				new() { Value = v }, w.Transaction
			)
			.AuditAsync(
				ok: x => { log.Dbg("New ID: {Id}", x.Value); repoIds.Add(x); },
				fail: log.Failure
			);
	}
}
Console.WriteLine();

// Retrieving values using repository
log.Dbg("== Retrieving values using repository ==");
using (var w = await db.StartWorkAsync())
{
	foreach (var x in repoIds)
	{
		await repo
			.RetrieveAsync<JsonEntity>(x)
			.AuditAsync(
				ok: x => log.Dbg("Found entity with ID: {Id} and Value: {@Value}", x.Id.Value, x.Value),
				fail: log.Failure
			);
	}

	await repo.Fluent()
		.WhereIn(x => x.Id, repoIds)
		.Sort(x => x.Id, SortOrder.Descending)
		.QueryAsync<JsonEntity>()
		.AuditAsync(
			ok: x => log.Dbg("Found: {@List}", x.ToList()),
			fail: log.Failure
		);

}
Console.WriteLine();

// Drop table
log.Dbg("== Dropping table == ");
await db
	.ExecuteAsync(
		$"DROP TABLE {jsonTable};", null, CommandType.Text
	);
Console.WriteLine();

//// Migrate auth tablse
//log.Dbg("== Migrate Auth ==");
//authDb.MigrateToLatest();

//// Insert user
//log.Dbg("== Insert User ==");
//var email = Rnd.Str;
//var userId = await auth.User
//	.CreateAsync(email, Rnd.Str)
//	.AuditAsync(
//		ok: x => log.Dbg("New User ID: {UserId}", x.Value),
//		fail: f => log.Failure(f)
//	)
//	.UnwrapAsync(x => x.Value(() => throw new Exception("Unable to get User ID.")));

//// Update user last sign in
//log.Dbg("== Update sign in ==");
//await auth.User
//	.UpdateLastSignInAsync(userId)
//	.AuditAsync(
//		ok: x => { if (x) { log.Dbg("Last sign in updated"); } else { log.Err("Unable to update last sign in"); } },
//		fail: f => log.Failure(f)
//	);

//// Don't insert duplicate user
//log.Dbg("== Don't insert duplicate user ==");
//await auth.User
//	.CreateAsync(email, Rnd.Str)
//	.AuditAsync(
//		ok: _ => log.Err("Should not have inserted duplicate user!"),
//		fail: f => log.Failure(f)
//	);

//// Clean up users
//log.Dbg("== Cleaning up user entities ==");
//await authDb.ExecuteAsync("TRUNCATE TABLE auth.user;", null, CommandType.Text);

// Done
log.Dbg("Done.");

Console.Read();
