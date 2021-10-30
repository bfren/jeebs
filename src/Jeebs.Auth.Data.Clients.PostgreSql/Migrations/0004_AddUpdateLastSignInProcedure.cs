// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.PostgreSql.Migrations;

/// <summary>
/// Migration: Add update last sign in procedure
/// </summary>
[Migration(4, "Add update last user sign in procedure")]
public sealed class AddUpdateLastSignInProcedure : Migration
{
	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up()
	{
		Execute(@"
			CREATE OR REPLACE PROCEDURE ""auth"".""UpdateUserLastSignIn""(
				id integer DEFAULT 0)
			LANGUAGE 'sql'
			AS $BODY$
			UPDATE ""auth"".""user"" SET ""UserLastSignedIn"" = NOW() WHERE ""UserId"" = id;
			$BODY$;
		");
	}

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down()
	{
		Execute(@"DROP PROCEDURE IF EXISTS ""auth"".""UpdateUserLastSignIn""(integer);;");
	}
}
