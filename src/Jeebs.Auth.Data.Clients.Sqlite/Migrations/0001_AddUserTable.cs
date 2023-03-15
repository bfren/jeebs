// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.Sqlite.Migrations;

/// <summary>
/// Migration: Add user table
/// </summary>
[Migration(1, "Add user table")]
public sealed class AddUserTable : Migration
{
	private string Col(Func<AuthUserTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE TABLE IF NOT EXISTS ""{AuthDb.Schema}.{AuthUserTable.TableName}"" (
			""{Col(u => u.Id)}"" INTEGER NOT NULL,
			""{Col(u => u.Version)}"" INTEGER NOT NULL DEFAULT 0,
			""{Col(u => u.EmailAddress)}"" TEXT NOT NULL UNIQUE,
			""{Col(u => u.PasswordHash)}"" TEXT NOT NULL,
			""{Col(u => u.TotpSecret)}"" TEXT,
			""{Col(u => u.TotpBackupCodes)}"" TEXT,
			""{Col(u => u.FriendlyName)}"" TEXT,
			""{Col(u => u.GivenName)}"" TEXT,
			""{Col(u => u.FamilyName)}"" TEXT,
			""{Col(u => u.IsEnabled)}"" INTEGER DEFAULT 0,
			""{Col(u => u.IsSuper)}"" INTEGER DEFAULT 0,
			""{Col(u => u.LastSignedIn)}"" TEXT,
			PRIMARY KEY (""{Col(u => u.Id)}"" AUTOINCREMENT)
		)
		;
	");

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down() => Execute($@"
		DROP TABLE IF EXISTS ""{AuthDb.Schema}.{AuthUserTable.TableName}""
		;
	");
}
