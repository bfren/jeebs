// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations;

/// <summary>
/// Migration: Add user table
/// </summary>
[Migration(1, "Add user table")]
public sealed class AddUserTable : Migration
{
	private string Col(Func<AuthUserTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE TABLE `{AuthDb.Schema}.{AuthUserTable.Name}` (
			`{Col(u => u.Id)}` INT(11) NOT NULL AUTO_INCREMENT,
			`{Col(u => u.Version)}` INT(11) NOT NULL DEFAULT '0',
			`{Col(u => u.EmailAddress)}` VARCHAR(128) NOT NULL COLLATE 'utf8_general_ci',
			`{Col(u => u.PasswordHash)}` VARCHAR(128) NOT NULL COLLATE 'utf8_general_ci',
			`{Col(u => u.TotpSecret)}` VARCHAR(64) NULL COLLATE 'utf8_general_ci',
			`{Col(u => u.TotpBackupCodes)}` VARCHAR(132) NULL COLLATE 'utf8_general_ci',
			`{Col(u => u.FriendlyName)}` VARCHAR(32) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
			`{Col(u => u.GivenName)}` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
			`{Col(u => u.FamilyName)}` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
			`{Col(u => u.IsEnabled)}` BIT(1) NOT NULL DEFAULT b'0',
			`{Col(u => u.IsSuper)}` BIT(1) NOT NULL DEFAULT b'0',
			`{Col(u => u.LastSignedIn)}` DATETIME NULL DEFAULT NULL,
			PRIMARY KEY (`{Col(u => u.Id)}`) USING BTREE,
			UNIQUE INDEX `{Col(u => u.EmailAddress)}` (`{Col(u => u.EmailAddress)}`) USING BTREE
		)
		COLLATE='utf8_general_ci'
		ENGINE=InnoDB
		;
	");

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down() => Execute($@"
		DROP TABLE `{AuthDb.Schema}.{AuthUserTable.Name}`
		;
	");
}
