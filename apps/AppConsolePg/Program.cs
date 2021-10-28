﻿// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using AppConsolePg;
using Jeebs;
using Jeebs.Data.Clients.PostgreSql.Parameters;
using Microsoft.Extensions.DependencyInjection;

await Jeebs.Apps.Program.MainAsync<App>(args, async (provider, log) =>
{
	// Begin
	log.Debug("= PostgreSQL Console Test =");

	// Get services
	var db = provider.GetRequiredService<Db>();
	Console.WriteLine();

	// Create table
	log.Debug("== Creating table ==");
	const string table = "console.test";
	await db
		.ExecuteAsync(
			$"CREATE TABLE IF NOT EXISTS {table} " +
			"(" +
			"id integer NOT NULL GENERATED ALWAYS AS IDENTITY, " +
			"foo text NOT NULL, " +
			"bar character(6)" +
			");",
			null,
			CommandType.Text
		);
	Console.WriteLine();

	// Insert into the table
	log.Debug(" == Inserting data ==");
	var foo = F.Rnd.Str;
	var bar = F.Rnd.Str;
	await db
		.ExecuteAsync(
			$"INSERT INTO {table} (foo, bar) VALUES (@foo, @bar);", new { foo, bar }, CommandType.Text
		)
		.AuditAsync(
			none: r => log.Message(r)
		);
	Console.WriteLine();

	// Get data from the table
	log.Debug("== Retrieving data ==");
	var id = 0;
	await db
		.QuerySingleAsync<Test>(
			$"SELECT * FROM {table} WHERE foo = @foo AND bar = @bar;", new { foo, bar }, CommandType.Text
		)
		.AuditAsync(
			some: x => { if (x.Foo == foo) { log.Debug("Succeeded: {@Test}.", x); id = x.Id; } else { log.Error("Failed."); } },
			none: r => log.Message(r)
		);
	Console.WriteLine();

	// Update data
	log.Debug("== Updating data ==");
	var newFoo = F.Rnd.Str;
	await db
		.ExecuteAsync(
			$"UPDATE {table} SET foo = @newFoo WHERE id = @id;", new { newFoo, id }, CommandType.Text
		)
		.AuditAsync(
			none: r => log.Message(r)
		);

	await db
		.QuerySingleAsync<Test>(
			$"SELECT * FROM {table} WHERE id = @id;", new { id }, CommandType.Text
		)
		.AuditAsync(
			some: x => { if (x.Foo == newFoo) { log.Debug("Succeeded: {@Test}.", x); } else { log.Error("Failed."); } },
			none: r => log.Message(r)
		);
	Console.WriteLine();

	// Delete data
	log.Debug("== Deleting data ==");
	await db
		.ExecuteAsync(
			$"DELETE FROM {table} WHERE id = @id;", new { id }, CommandType.Text
		)
		.AuditAsync(
			some: x => { if (x) { log.Debug("Succeeded."); } else { log.Error("Failed."); } },
			none: r => log.Message(r)
		);
	Console.WriteLine();

	// Drop table
	log.Debug("== Dropping table == ");
	await db
		.ExecuteAsync(
			$"DROP TABLE {table};", null, CommandType.Text
		);
	Console.WriteLine();

	// Create table
	log.Debug("== Creating table with JSON ==");
	const string jsonTable = "console.test_json";
	await db
		.ExecuteAsync(
			$"CREATE TABLE IF NOT EXISTS {jsonTable} " +
			"(" +
			"id integer NOT NULL GENERATED ALWAYS AS IDENTITY, " +
			"value jsonb NOT NULL" +
			");",
			null,
			CommandType.Text
		);
	Console.WriteLine();

	// Insert value
	log.Debug("== Inserting values with JSON ==");
	var v0 = new Test(7, F.Rnd.Str, F.Rnd.Str);
	var v1 = new Test(18, F.Rnd.Str, F.Rnd.Str);
	var v2 = new Test(93, F.Rnd.Str, F.Rnd.Str);
	foreach (var v in new[] { v0, v1, v2 })
	{
		await db
			.ExecuteAsync(
				$"INSERT INTO {jsonTable} (value) VALUES (@value);", new { value = Jsonb.Create(v) }, CommandType.Text
			)
			.AuditAsync(
				none: r => log.Message(r)
			);
	}
	Console.WriteLine();

	log.Debug("== Checking JSON insert has worked ==");
	var value = await db
		.QuerySingleAsync<int>(
			$"SELECT value -> '{nameof(Test.Id).ToCamelCase()}' FROM {jsonTable} WHERE value ->> '{nameof(Test.Foo).ToCamelCase()}' = @foo;", new { foo = v1.Foo }, CommandType.Text
		)
		.AuditAsync(
			some: x => { if (x == 18) { log.Debug("Succeeded: {@Test}.", x); } else { log.Error("Failed."); } },
			none: r => log.Message(r)
		);
	Console.WriteLine();

	// Drop table
	log.Debug("== Dropping table == ");
	await db
		.ExecuteAsync(
			$"DROP TABLE {jsonTable};", null, CommandType.Text
		);
	Console.WriteLine();

	// Done
	log.Debug("Done.");
});

record struct Test(int Id, string Foo, string Bar);
