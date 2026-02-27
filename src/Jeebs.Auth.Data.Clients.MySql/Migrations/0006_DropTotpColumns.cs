// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.MySql.Migrations;

/// <summary>
/// Migration: Drop TOTP columns
/// </summary>
[Migration(6, "Drop TOTP columns")]
public sealed class DropTotpColumns : Migration
{
	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up() => Execute($@"
		ALTER TABLE `{AuthDb.Schema}.{AuthUserTable.TableName}`
			DROP COLUMN `user_totp_secret`,
			DROP COLUMN `user_totp_backup_codes`
		;
	");

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down() => Execute($@"
		ALTER TABLE `{AuthDb.Schema}.{AuthUserTable.TableName}`
			ADD COLUMN `user_totp_secret` VARCHAR(64) NULL COLLATE 'utf8_general_ci',
			ADD COLUMN `user_totp_backup_codes` VARCHAR(132) NULL COLLATE 'utf8_general_ci'
		;
	");
}
