// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations
{
	/// <summary>
	/// Migration: Add user role table
	/// </summary>
	[Migration(3, "Add user role table")]
	public sealed class AddUserRoleTable : Migration
	{
		/// <summary>
		/// Migrate up
		/// </summary>
		protected override void Up()
		{
			Execute(@"
				CREATE TABLE `auth_user_role` (
					`UserRoleId` INT(11) NOT NULL AUTO_INCREMENT,
					`UserId` INT(11) NOT NULL,
					`RoleId` INT(11) NOT NULL,
					PRIMARY KEY (`UserRoleId`) USING BTREE
				)
				COLLATE='utf8_general_ci'
				ENGINE=InnoDB
				;
			");
		}

		/// <summary>
		/// Migrate down
		/// </summary>
		protected override void Down()
		{
			Execute("DROP TABLE `auth_user_role`;");
		}
	}
}
