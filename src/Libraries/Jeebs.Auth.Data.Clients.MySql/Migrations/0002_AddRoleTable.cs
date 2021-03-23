// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations
{
	[Migration(2, "Add role table")]
	public sealed class AddRoleTable : Migration
	{
		protected override void Up()
		{
			Execute(@"
				CREATE TABLE `auth_role` (
					`RoleId` INT(11) NOT NULL AUTO_INCREMENT,
					`RoleName` VARCHAR(64) NOT NULL COLLATE 'utf8_general_ci',
					`RoleDescription` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
					PRIMARY KEY (`RoleId`) USING BTREE
				)
				COLLATE='utf8_general_ci'
				ENGINE=InnoDB
			");
		}

		protected override void Down()
		{
			Execute("DROP TABLE `auth_role`;");
		}
	}
}
