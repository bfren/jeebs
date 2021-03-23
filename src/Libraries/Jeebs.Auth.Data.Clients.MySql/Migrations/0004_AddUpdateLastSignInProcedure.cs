// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations
{
	[Migration(4, "Add user role table")]
	public sealed class AddUpdateLastSignInProcedure : Migration
	{
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

		protected override void Down()
		{
			Execute("DROP PROCEDURE `UpdateUserLastSignIn`;");
		}
	}
}
