// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using AppConsolePg;
using Jeebs;
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
	const string table = "\"console\".\"test\"";
	await db
		.ExecuteAsync(
			$"CREATE TABLE IF NOT EXISTS {table} " +
			"(" +
			"\"id\" integer NOT NULL GENERATED ALWAYS AS IDENTITY, " +
			"\"foo\" text NOT NULL, " +
			"\"bar\" character(6)" +
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
			$"INSERT INTO {table} (\"foo\", \"bar\") VALUES (@foo, @bar);", new { foo, bar }, CommandType.Text
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
			$"SELECT * FROM {table} WHERE \"foo\" = @foo AND \"bar\" = @bar;", new { foo, bar }, CommandType.Text
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
			$"UPDATE {table} SET \"foo\" = @newFoo WHERE \"id\" = @id;", new { newFoo, id }, CommandType.Text
		)
		.AuditAsync(
			none: r => log.Message(r)
		);

	await db
		.QuerySingleAsync<Test>(
			$"SELECT * FROM {table} WHERE \"id\" = @id;", new { id }, CommandType.Text
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
			$"DELETE FROM {table} WHERE \"id\" = @id;", new { id }, CommandType.Text
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

	// Done
	log.Debug("Done.");
});

record struct Test(int Id, string Foo, string Bar);
record struct Json(string Val);
