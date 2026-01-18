// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations;

/// <summary>
/// Migration: Drop update last sign in procedure.
/// </summary>
[Migration(5, "Drop update last user sign in procedure")]
public sealed class DropUpdateLastSignInProcedure : Migration
{
	private string Col(Func<AuthUserTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up.
	/// </summary>
	protected override void Up() => Execute($@"
		DROP FUNCTION `{AuthDb.Schema}.{Procedures.UpdateUserLastSignIn}`
		;
	");

	/// <summary>
	/// Migrate down.
	/// </summary>
	protected override void Down() => Execute($@"
		CREATE DEFINER=`root`@`localhost` FUNCTION `{AuthDb.Schema}.{Procedures.UpdateUserLastSignIn}`(
			`id` BIGINT
		)
		RETURNS tinyint(1)
		LANGUAGE SQL
		NOT DETERMINISTIC
		CONTAINS SQL
		SQL SECURITY DEFINER
		COMMENT ''
		BEGIN

		UPDATE `{AuthDb.Schema}.{AuthUserTable.TableName}`
		SET `{Col(u => u.LastSignedIn)}` = NOW()
		WHERE `{Col(u => u.Id)}` = id;
		RETURN ROW_COUNT() > 0;

		END
	");
}
