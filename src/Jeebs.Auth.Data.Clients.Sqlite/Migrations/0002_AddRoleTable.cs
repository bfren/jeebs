// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.Sqlite.Migrations;

/// <summary>
/// Migration: Add role table.
/// </summary>
[Migration(2, "Add role table")]
public sealed class AddRoleTable : Migration
{
	private string Col(Func<AuthRoleTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up.
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE TABLE ""{AuthDb.Schema}.{AuthRoleTable.TableName}"" (
			""{Col(r => r.Id)}"" INTEGER NOT NULL UNIQUE,
			""{Col(r => r.Name)}"" TEXT NOT NULL,
			""{Col(r => r.Description)}"" TEXT,
			PRIMARY KEY (""{Col(r => r.Id)}"" AUTOINCREMENT)
		)
		;
	");

	/// <summary>
	/// Migrate down.
	/// </summary>
	protected override void Down() => Execute($@"
		DROP TABLE ""{AuthDb.Schema}.{AuthRoleTable.TableName}""
		;
	");
}
