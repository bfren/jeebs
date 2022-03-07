// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using AppConsolePg;
using Jeebs;
using Jeebs.Data.Clients.PostgreSql.Parameters;
using Jeebs.Data.TypeHandlers;
using Microsoft.Extensions.DependencyInjection;

await Jeebs.Apps.Program.MainAsync<App>(args, async (provider, log) =>
{
	// Begin
	log.Dbg("= PostgreSQL Console Test =");

	// Get services
	var db = provider.GetRequiredService<Db>();
	var repo = provider.GetRequiredService<Repository>();
	Console.WriteLine();

	// Create table
	log.Dbg("== Creating table ==");
	const string table = "console.test";
	_ = await db
		.ExecuteAsync(
			$"CREATE TABLE IF NOT EXISTS {table} " +
			"(" +
			"id integer NOT NULL GENERATED ALWAYS AS IDENTITY, " +
			"foo text NOT NULL, " +
			"bar character(6)" +
			");",
			null,
			CommandType.Text
		)
		.ConfigureAwait(false);
	Console.WriteLine();

	// Insert into the table
	log.Dbg(" == Inserting data ==");
	var foo = F.Rnd.Str;
	var bar = F.Rnd.Str;
	_ = await db
		.ExecuteAsync(
			$"INSERT INTO {table} (foo, bar) VALUES (@foo, @bar);", new { foo, bar }, CommandType.Text
		)
		.AuditAsync(
			none: r => log.Msg(r)
		)
		.ConfigureAwait(false);
	Console.WriteLine();

	// Get data from the table
	log.Dbg("== Retrieving data ==");
	var id = 0;
	_ = await db
		.QuerySingleAsync<ParamTest>(
			$"SELECT * FROM {table} WHERE foo = @foo AND bar = @bar;", new { foo, bar }, CommandType.Text
		)
		.AuditAsync(
			some: x => { if (x.Foo == foo) { log.Dbg("Succeeded: {@Test}.", x); id = x.Id; } else { log.Err("Failed."); } },
			none: r => log.Msg(r)
		)
		.ConfigureAwait(false);
	Console.WriteLine();

	// Update data
	log.Dbg("== Updating data ==");
	var newFoo = F.Rnd.Str;
	_ = await db
		.ExecuteAsync(
			$"UPDATE {table} SET foo = @newFoo WHERE id = @id;", new { newFoo, id }, CommandType.Text
		)
		.AuditAsync(
			none: r => log.Msg(r)
		)
		.ConfigureAwait(false);

	_ = await db
		.QuerySingleAsync<ParamTest>(
			$"SELECT * FROM {table} WHERE id = @id;", new { id }, CommandType.Text
		)
		.AuditAsync(
			some: x => { if (x.Foo == newFoo) { log.Dbg("Succeeded: {@Test}.", x); } else { log.Err("Failed."); } },
			none: r => log.Msg(r)
		)
		.ConfigureAwait(false);
	Console.WriteLine();

	// Delete data
	log.Dbg("== Deleting data ==");
	_ = await db
		.ExecuteAsync(
			$"DELETE FROM {table} WHERE id = @id;", new { id }, CommandType.Text
		)
		.AuditAsync(
			some: x => { if (x) { log.Dbg("Succeeded."); } else { log.Err("Failed."); } },
			none: r => log.Msg(r)
		)
		.ConfigureAwait(false);
	Console.WriteLine();

	// Drop table
	log.Dbg("== Dropping table == ");
	_ = await db
		.ExecuteAsync(
			$"DROP TABLE {table};", null, CommandType.Text
		)
		.ConfigureAwait(false);
	Console.WriteLine();

	// Create table
	log.Dbg("== Creating table with JSON ==");
	const string jsonTable = "console.test_json";
	_ = await db
		.ExecuteAsync(
			$"CREATE TABLE IF NOT EXISTS {jsonTable} " +
			"(" +
			"\"Id\" integer NOT NULL GENERATED ALWAYS AS IDENTITY, " +
			"\"Value\" jsonb NOT NULL" +
			");",
			null,
			CommandType.Text
		)
		.ConfigureAwait(false);
	Console.WriteLine();

	// Insert values using Jsonb
	log.Dbg("== Inserting values as Jsonb ==");
	var v0 = new ParamTest(7, F.Rnd.Str, F.Rnd.Str);
	var v1 = new ParamTest(18, F.Rnd.Str, F.Rnd.Str);
	var v2 = new ParamTest(93, F.Rnd.Str, F.Rnd.Str);
	using (var w = db.UnitOfWork)
	{
		foreach (var v in new[] { v0, v1, v2 })
		{
			_ = await db
				.ExecuteAsync(
					$"INSERT INTO {jsonTable} (\"Value\") VALUES (@value);", new { value = Jsonb.Create(v) }, CommandType.Text, w.Transaction
				)
				.AuditAsync(
					none: r => log.Msg(r)
				)
				.ConfigureAwait(false);
		}
	}
	Console.WriteLine();

	log.Dbg("== Checking Jsonb insert has worked ==");
	_ = await db
		.QuerySingleAsync<int>(
			$"SELECT \"Value\" -> '{nameof(ParamTest.Id).ToCamelCase()}' FROM {jsonTable} WHERE \"Value\" ->> '{nameof(ParamTest.Foo).ToCamelCase()}' = @foo;", new { foo = v1.Foo }, CommandType.Text
		)
		.AuditAsync(
			some: x => { if (x == 18) { log.Dbg("Succeeded: {@Test}.", x); } else { log.Err("Failed."); } },
			none: r => log.Msg(r)
		)
		.ConfigureAwait(false);
	Console.WriteLine();

	// Select values using Mapping
	log.Dbg("== Selecting values using mapping ==");
	Dapper.SqlMapper.AddTypeHandler(new JsonbTypeHandler<ParamTest>()); // add here so doesn't interfere with earlier tests

	_ = await db
		.QuerySingleAsync<EntityTest>(
			$"SELECT * FROM {jsonTable} WHERE \"Value\" ->> '{nameof(ParamTest.Foo).ToCamelCase()}' = @foo;", new { foo = v1.Foo }, CommandType.Text
		)
		.AuditAsync(
			some: x => { if (x.Value.Id == 18) { log.Dbg("Succeeded: {@Test}.", x); } else { log.Err("Failed."); } },
			none: r => log.Msg(r)
		)
		.ConfigureAwait(false);
	Console.WriteLine();

	// Insert values using repository
	log.Dbg("== Inserting values using repository ==");
	var v3 = new ParamTest(8, F.Rnd.Str, F.Rnd.Str);
	var v4 = new ParamTest(19, F.Rnd.Str, F.Rnd.Str);
	var v5 = new ParamTest(94, F.Rnd.Str, F.Rnd.Str);
	using (var w = db.UnitOfWork)
	{
		foreach (var v in new[] { v3, v4, v5 })
		{
			_ = await repo
				.CreateAsync(
					new() { Id = new(), Value = v }, w.Transaction
				)
				.AuditAsync(
					some: x => log.Dbg("New ID: {Id}", x.Value),
					none: r => log.Msg(r)
				)
				.ConfigureAwait(false);
		}
	}
	Console.WriteLine();

	// Drop table
	log.Dbg("== Dropping table == ");
	_ = await db
		.ExecuteAsync(
			$"DROP TABLE {jsonTable};", null, CommandType.Text
		)
		.ConfigureAwait(false);
	Console.WriteLine();

	// Done
	log.Dbg("Done.");
}).ConfigureAwait(false);
