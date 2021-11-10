// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.Mapping;

/// <inheritdoc cref="ITableName"/>
public sealed record class TableName : ITableName
{
	/// <summary>
	/// Schema Separator
	/// </summary>
	public const char SchemaSeparator = '.';

	/// <inheritdoc/>
	public string? Schema { get; init; }

	/// <inheritdoc/>
	public string Name { get; init; }

	/// <summary>
	/// Create with only a name
	/// </summary>
	/// <param name="name">Table Name</param>
	public TableName(string name) =>
		Name = name;

	/// <summary>
	/// Create with schema and name
	/// </summary>
	/// <param name="schema">Table Schema</param>
	/// <param name="name">Table Name</param>
	public TableName(string schema, string name) : this(name) =>
		Schema = schema;

	/// <inheritdoc/>
	public string GetFullName(Func<string, string> escape) =>
		Schema switch
		{
			{ } =>
				$"{escape(Schema)}{SchemaSeparator}{escape(Name)}",

			_ =>
				escape(Name)
		};
}
