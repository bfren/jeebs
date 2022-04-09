// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.Map;

/// <inheritdoc cref="IDbName"/>
public sealed record class DbName : IDbName
{
	/// <summary>
	/// Schema Separator
	/// </summary>
	public static readonly char SchemaSeparator = '.';

	/// <inheritdoc/>
	public string? Schema { get; init; }

	/// <inheritdoc/>
	public string Name { get; init; }

	/// <summary>
	/// Create with only a name
	/// </summary>
	/// <param name="name">Database object name</param>
	public DbName(string name) =>
		Name = name;

	/// <summary>
	/// Create with schema and name
	/// </summary>
	/// <param name="schema">Database schema</param>
	/// <param name="name">Database object name</param>
	public DbName(string schema, string name) : this(name) =>
		Schema = schema;

	/// <inheritdoc/>
	public string GetFullName(Func<string, string> escape) =>
		GetFullName(escape, SchemaSeparator);

	/// <inheritdoc/>
	public string GetFullName(Func<string, string> escape, char schemaSeparator) =>
		Schema switch
		{
			{ } =>
				$"{escape(Schema)}{schemaSeparator}{escape(Name)}",

			_ =>
				escape(Name)
		};
}
