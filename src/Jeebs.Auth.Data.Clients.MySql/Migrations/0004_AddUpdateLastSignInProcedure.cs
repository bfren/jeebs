// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations
{
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
				CREATE DEFINER=`ben`@`%` PROCEDURE `UpdateUserLastSignIn`(
					IN `Id` BIGINT
				)
				LANGUAGE SQL
				NOT DETERMINISTIC
				CONTAINS SQL
				SQL SECURITY DEFINER
				COMMENT ''
				BEGIN

				UPDATE `auth_user` SET `UserLastSignedIn` = NOW() WHERE `UserId` = Id;

				END
			");
		}

		/// <summary>
		/// Migrate down
		/// </summary>
		protected override void Down()
		{
			Execute("DROP PROCEDURE `UpdateUserLastSignIn`;");
		}
	}
}
