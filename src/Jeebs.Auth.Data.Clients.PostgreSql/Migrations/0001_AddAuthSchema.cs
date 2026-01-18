// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.PostgreSql.Migrations;

/// <summary>
/// Migration: Add auth schema.
/// </summary>
[Migration(1, "Add auth schema")]
public sealed class AddAuthSchema : Migration
{
	/// <summary>
	/// Migrate up.
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE SCHEMA IF NOT EXISTS {AuthDb.Schema}
		;
	");

	/// <summary>
	/// Migrate down.
	/// </summary>
	protected override void Down()
	{
		// Can't remove schema as VersionInfo table still exists
	}
}
