// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations;

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
			CREATE DEFINER=`%`@`%` PROCEDURE `Auth.UpdateUserLastSignIn`(
				IN `Id` BIGINT
			)
			LANGUAGE SQL
			NOT DETERMINISTIC
			CONTAINS SQL
			SQL SECURITY DEFINER
			COMMENT ''
			BEGIN

			UPDATE `Auth`.`User` SET `UserLastSignedIn` = NOW() WHERE `UserId` = Id;

			END
		");
	}

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down()
	{
		Execute("DROP PROCEDURE `Auth.UpdateUserLastSignIn`;");
	}
}
