// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.Sqlite.Migrations;

/// <summary>
/// Migration: Add RequiresRehash column
/// </summary>
[Migration(5, "Add RequiresRehash column")]
public sealed class AddRequiresRehashColumn : Migration
{
	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up() => Execute($@"
		ALTER TABLE ""{AuthDb.Schema}.{AuthUserTable.TableName}""
			ADD COLUMN ""user_requires_rehash"" BOOLEAN DEFAULT TRUE
		;
	");

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down() => Execute($@"
		ALTER TABLE ""{AuthDb.Schema}.{AuthUserTable.TableName}""
			DROP COLUMN ""user_requires_rehash""
		;
	");
}
