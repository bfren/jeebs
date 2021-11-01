// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.PostgreSql.Migrations;

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
			CREATE TABLE IF NOT EXISTS ""Auth"".""Role""
			(
				""RoleId"" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
				""RoleName"" character(64) COLLATE pg_catalog.default NOT NULL,
				""RoleDescription"" character(128) COLLATE pg_catalog.default NOT NULL,
				CONSTRAINT ""RoleId_Key"" PRIMARY KEY(""RoleId""),
				CONSTRAINT ""RoleName_Unique"" UNIQUE(""RoleName"")
			)
			TABLESPACE pg_default
			;
		");
	}

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down()
	{
		Execute(@"DROP TABLE IF EXISTS ""Auth"".""Role"";");
	}
}
