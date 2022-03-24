// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.PostgreSql.Migrations;

/// <summary>
/// Migration: Add user table
/// </summary>
[Migration(2, "Add user table")]
public sealed class AddUserTable : Migration
{
	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up()
	{
		Execute(@"
			CREATE TABLE IF NOT EXISTS ""Auth"".""User""
			(
				""UserId"" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
				""UserVersion"" integer NOT NULL DEFAULT 0,
				""UserEmailAddress"" character(128) COLLATE pg_catalog.default NOT NULL,
				""UserPasswordHash"" character(128) COLLATE pg_catalog.default NOT NULL,
				""UserTotpSecret"" character(64) COLLATE pg_catalog.default,
				""UserTotpBackupCodes"" character(132) COLLATE pg_catalog.default,
				""UserFriendlyName"" character(32) COLLATE pg_catalog.default,
				""UserGivenName"" character(128) COLLATE pg_catalog.default,
				""UserFamilyName"" character(128) COLLATE pg_catalog.default,
				""UserIsEnabled"" boolean NOT NULL DEFAULT false,
				""UserIsSuper"" boolean NOT NULL DEFAULT false,
				""UserLastSignedIn"" timestamp without time zone,
				CONSTRAINT ""UserId_Key"" PRIMARY KEY(""UserId""),
				CONSTRAINT ""UserEmailAddress_Unique"" UNIQUE(""UserEmailAddress"")
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
		Execute(@"DROP TABLE IF EXISTS ""Auth"".""User"";");
	}
}
