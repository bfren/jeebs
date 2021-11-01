// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations;

/// <summary>
/// Migration: Add role table
/// </summary>
[Migration(2, "Add role table")]
public sealed class AddRoleTable : Migration
{
	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up()
	{
		Execute(@"
			CREATE TABLE `Auth.Role` (
				`RoleId` INT(11) NOT NULL AUTO_INCREMENT,
				`RoleName` VARCHAR(64) NOT NULL COLLATE 'utf8_general_ci',
				`RoleDescription` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
				PRIMARY KEY (`RoleId`) USING BTREE
			)
			COLLATE='utf8_general_ci'
			ENGINE=InnoDB
		");
	}

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down()
	{
		Execute("DROP TABLE `Auth.Role`;");
	}
}
