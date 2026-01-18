// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.Sqlite.Migrations;

/// <summary>
/// Migration: Add user role table.
/// </summary>
[Migration(3, "Add user role table")]
public sealed class AddUserRoleTable : Migration
{
	private string Col(Func<AuthUserRoleTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up.
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE TABLE ""{AuthDb.Schema}.{AuthUserRoleTable.TableName}"" (
			""{Col(ur => ur.Id)}"" INTEGER NOT NULL UNIQUE,
			""{Col(ur => ur.UserId)}"" INTEGER NOT NULL,
			""{Col(ur => ur.RoleId)}"" INTEGER NOT NULL,
			PRIMARY KEY (""{Col(ur => ur.Id)}"" AUTOINCREMENT)
		)
		;
	");

	/// <summary>
	/// Migrate down.
	/// </summary>
	protected override void Down() => Execute($@"
		DROP TABLE ""{AuthDb.Schema}.{AuthUserRoleTable.TableName}""
		;
	");
}
