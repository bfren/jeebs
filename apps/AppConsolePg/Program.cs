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

	// Insert into the table
	log.Debug("== Inserting data ==");
	var foo = F.Rnd.Str;
	var bar = F.Rnd.Str;
	await db
		.ExecuteAsync(
			"INSERT INTO \"console\".\"test\" (\"foo\", \"bar\") VALUES (@foo, @bar);", new { foo, bar }, CommandType.Text
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
			"SELECT * FROM \"console\".\"test\" WHERE \"foo\" = @foo AND \"bar\" = @bar;", new { foo, bar }, CommandType.Text
		)
		.AuditAsync(
			some: x =>
			{
				log.Debug("Found row: {@Row}", x);
				id = x.Id;
			},
			none: r => log.Message(r)
		);
	Console.WriteLine();

	// Update data
	log.Debug("== Updating data ==");
	var newFoo = F.Rnd.Str;
	await db
		.ExecuteAsync(
			"UPDATE \"console\".\"test\" SET \"foo\" = @newFoo WHERE \"id\" = @id;", new { newFoo, id }, CommandType.Text
		)
		.AuditAsync(
			none: r => log.Message(r)
		);

	await db
		.QuerySingleAsync<Test>(
			"SELECT * FROM \"console\".\"test\" WHERE \"id\" = @id;", new { id }, CommandType.Text
		)
		.AuditAsync(
			some: x =>
			{
				if (x.Foo == newFoo)
				{
					log.Debug("Succeeded.");
				}
				else
				{
					log.Error("Failed.");
				}
			},
			none: r => log.Message(r)
		);
});

record struct Test(int Id, string Foo, string Bar);
