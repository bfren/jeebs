// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations;

/// <summary>
/// Migration: Add user role table
/// </summary>
[Migration(3, "Add user role table")]
public sealed class AddUserRoleTable : Migration
{
	private string Col(Func<AuthUserRoleTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE TABLE `{AuthDb.Schema}.{AuthUserRoleTable.Name}` (
			`{Col(ur => ur.Id)}` INT(11) NOT NULL AUTO_INCREMENT,
			`{Col(ur => ur.UserId)}` INT(11) NOT NULL,
			`{Col(ur => ur.RoleId)}` INT(11) NOT NULL,
			PRIMARY KEY (`{Col(ur => ur.Id)}`) USING BTREE
		)
		COLLATE='utf8_general_ci'
		ENGINE=InnoDB
		;
	");

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down() => Execute($@"
		DROP TABLE `{AuthDb.Schema}.{AuthUserRoleTable.Name}`
		;
	");
}
