// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.PostgreSql.Migrations;

/// <summary>
/// Migration: Add role table.
/// </summary>
[Migration(3, "Add role table")]
public sealed class AddRoleTable : Migration
{
	private string Col(Func<AuthRoleTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up.
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE TABLE IF NOT EXISTS {AuthDb.Schema}.{AuthRoleTable.TableName}
		(
			{Col(r => r.Id)} integer NOT NULL GENERATED ALWAYS AS IDENTITY,
			{Col(r => r.Name)} text COLLATE pg_catalog.default NOT NULL,
			{Col(r => r.Description)} text COLLATE pg_catalog.default NOT NULL,
			CONSTRAINT {Col(r => r.Id)}_key PRIMARY KEY({Col(r => r.Id)}),
			CONSTRAINT {Col(r => r.Name)}_unique UNIQUE({Col(r => r.Name)})
		)
		TABLESPACE pg_default
		;
	");

	/// <summary>
	/// Migrate down.
	/// </summary>
	protected override void Down() => Execute($@"
		DROP TABLE IF EXISTS {AuthDb.Schema}.{AuthRoleTable.TableName}
		;
	");
}
