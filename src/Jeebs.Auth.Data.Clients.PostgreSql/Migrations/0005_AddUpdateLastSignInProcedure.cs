// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.PostgreSql.Migrations;

/// <summary>
/// Migration: Add update last sign in procedure
/// </summary>
[Migration(5, "Add update last user sign in procedure")]
public sealed class AddUpdateLastSignInProcedure : Migration
{
	private string Col(Func<AuthUserTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE OR REPLACE PROCEDURE ""{AuthDb.Schema}"".""UpdateUserLastSignIn""(
			""Id"" integer DEFAULT 0
		)
		LANGUAGE 'sql'
		AS $BODY$
		UPDATE ""{AuthDb.Schema}"".""{AuthUserTable.TableName}""
		SET ""{Col(u => u.LastSignedIn)}"" = NOW()
		WHERE ""{Col(u => u.Id)}"" = ""Id"";
		$BODY$
		;
	");

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down() => Execute($@"
		DROP PROCEDURE IF EXISTS ""{AuthDb.Schema}"".""UpdateUserLastSignIn""(integer)
		;
	");
}
