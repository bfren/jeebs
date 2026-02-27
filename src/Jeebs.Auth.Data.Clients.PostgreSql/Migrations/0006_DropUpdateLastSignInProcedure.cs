// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.PostgreSql.Migrations;

/// <summary>
/// Migration: Drop update last sign in procedure
/// </summary>
[Migration(6, "Drop update last user sign in procedure")]
public sealed class DropUpdateLastSignInProcedure : Migration
{
	private string Col(Func<AuthUserTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE OR REPLACE FUNCTION {AuthDb.Schema}.{Procedures.UpdateUserLastSignIn}(
			id bigint DEFAULT 0)
			RETURNS boolean
			LANGUAGE 'plpgsql'
			COST 100
			VOLATILE PARALLEL UNSAFE
		AS $BODY$
		BEGIN
			UPDATE {AuthDb.Schema}.{AuthUserTable.TableName}
			SET {Col(u => u.LastSignedIn)} = NOW()
			WHERE {Col(u => u.Id)} = id;
			RETURN FOUND;
		END;
		$BODY$
		;
	");

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down() => Execute($@"
		DROP FUNCTION IF EXISTS {AuthDb.Schema}.{Procedures.UpdateUserLastSignIn}(bigint)
		;
	");
}
