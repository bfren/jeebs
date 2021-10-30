// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.PostgreSql.Migrations;

/// <summary>
/// Migration: Add user role table
/// </summary>
[Migration(3, "Add user role table")]
public sealed class AddUserRoleTable : Migration
{
	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up()
	{
		Execute(@"
			CREATE TABLE ""auth"".""user_role""
			(
				""UserRoleId"" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
				""UserId"" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
				""RoleId"" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
				CONSTRAINT ""UserRoleId_Key"" PRIMARY KEY(""UserRoleId"")
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
		Execute(@"DROP TABLE IF EXISTS ""auth"".""user_role"";");
	}
}
