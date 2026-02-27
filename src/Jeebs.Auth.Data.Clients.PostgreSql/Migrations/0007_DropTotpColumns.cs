// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.PostgreSql.Migrations;

/// <summary>
/// Migration: Drop TOTP columns
/// </summary>
[Migration(7, "Drop TOTP columns")]
public sealed class DropTotpColumns : Migration
{
	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up() => Execute($@"
		ALTER TABLE {AuthDb.Schema}.{AuthUserTable.TableName}
			DROP COLUMN user_totp_secret,
			DROP COLUMN user_totp_backup_codes
		;
	");

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down() => Execute($@"
		ALTER TABLE {AuthDb.Schema}.{AuthUserTable.TableName}
			ADD COLUMN user_totp_secret text COLLATE pg_catalog.default,
			ADD COLUMN user_totp_backup_codes text COLLATE pg_catalog.default
		;
	");
}
