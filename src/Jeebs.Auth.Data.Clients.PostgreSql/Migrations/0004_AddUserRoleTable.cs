// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.PostgreSql.Migrations;

/// <summary>
/// Migration: Add user role table
/// </summary>
[Migration(4, "Add user role table")]
public sealed class AddUserRoleTable : Migration
{
	private string Col(Func<AuthUserRoleTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE TABLE ""{AuthDb.Schema}"".""{AuthUserRoleTable.TableName}""
		(
			""{Col(ur => ur.Id)}"" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
			""{Col(ur => ur.UserId)}"" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
			""{Col(ur => ur.RoleId)}"" integer NOT NULL GENERATED ALWAYS AS IDENTITY,
			CONSTRAINT ""{Col(ur => ur.Id)}_Key"" PRIMARY KEY(""{Col(ur => ur.Id)}"")
		)
		TABLESPACE pg_default
		;
	");

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down() => Execute($@"
		DROP TABLE IF EXISTS ""{AuthDb.Schema}"".""{AuthUserRoleTable.TableName}""
		;
	");
}
