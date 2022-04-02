// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations;

/// <summary>
/// Migration: Add role table
/// </summary>
[Migration(2, "Add role table")]
public sealed class AddRoleTable : Migration
{
	private string Col(Func<AuthRoleTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE TABLE `{AuthDb.Schema}.{AuthRoleTable.TableName}` (
			`{Col(r => r.Id)}` INT(11) NOT NULL AUTO_INCREMENT,
			`{Col(r => r.Name)}` VARCHAR(64) NOT NULL COLLATE 'utf8_general_ci',
			`{Col(r => r.Description)}` VARCHAR(128) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
			PRIMARY KEY (`{Col(r => r.Id)}`) USING BTREE
		)
		COLLATE='utf8_general_ci'
		ENGINE=InnoDB
	");

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down() => Execute($@"
		DROP TABLE `{AuthDb.Schema}.{AuthRoleTable.TableName}`
		;
	");
}
