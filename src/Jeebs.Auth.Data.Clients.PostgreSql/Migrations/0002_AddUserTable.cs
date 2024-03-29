// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Tables;
using SimpleMigrations;

namespace Jeebs.Auth.Data.Clients.PostgreSql.Migrations;

/// <summary>
/// Migration: Add user table
/// </summary>
[Migration(2, "Add user table")]
public sealed class AddUserTable : Migration
{
	private string Col(Func<AuthUserTable, string> selector) =>
		selector(new());

	/// <summary>
	/// Migrate up
	/// </summary>
	protected override void Up() => Execute($@"
		CREATE TABLE IF NOT EXISTS {AuthDb.Schema}.{AuthUserTable.TableName}
		(
			{Col(u => u.Id)} integer NOT NULL GENERATED ALWAYS AS IDENTITY,
			{Col(u => u.Version)} integer NOT NULL DEFAULT 0,
			{Col(u => u.EmailAddress)} text COLLATE pg_catalog.default NOT NULL,
			{Col(u => u.PasswordHash)} text COLLATE pg_catalog.default NOT NULL,
			{Col(u => u.TotpSecret)} text COLLATE pg_catalog.default,
			{Col(u => u.TotpBackupCodes)} text COLLATE pg_catalog.default,
			{Col(u => u.FriendlyName)} text COLLATE pg_catalog.default,
			{Col(u => u.GivenName)} text COLLATE pg_catalog.default,
			{Col(u => u.FamilyName)} text COLLATE pg_catalog.default,
			{Col(u => u.IsEnabled)} boolean NOT NULL DEFAULT false,
			{Col(u => u.IsSuper)} boolean NOT NULL DEFAULT false,
			{Col(u => u.LastSignedIn)} timestamp with time zone,
			CONSTRAINT {Col(u => u.Id)}_key PRIMARY KEY({Col(u => u.Id)}),
			CONSTRAINT {Col(u => u.EmailAddress)}_unique UNIQUE({Col(u => u.EmailAddress)})
		)
		TABLESPACE pg_default
		;
	");

	/// <summary>
	/// Migrate down
	/// </summary>
	protected override void Down() => Execute($@"
		DROP TABLE IF EXISTS {AuthDb.Schema}.{AuthUserTable.TableName}
		;
	");
}
