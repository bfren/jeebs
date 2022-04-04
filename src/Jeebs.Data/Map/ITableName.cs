// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.Map;

/// <summary>
/// Table name and optional schema
/// </summary>
public interface ITableName
{
	/// <summary>
	/// [Optional] Table Schema
	/// </summary>
	string? Schema { get; }

	/// <summary>
	/// Table Name
	/// </summary>
	string Name { get; }

	/// <summary>
	/// Escape and return table <see cref="Schema"/> and <see cref="Name"/> separated by '.',
	/// or simply the <see cref="Name"/> if <see cref="Schema"/> is null
	/// </summary>
	/// <param name="escape">Escape function</param>
	string GetFullName(Func<string, string> escape);

	/// <summary>
	/// Escape and return table <see cref="Schema"/> and <see cref="Name"/> separated by '.',
	/// or simply the <see cref="Name"/> if <see cref="Schema"/> is null
	/// </summary>
	/// <param name="escape">Escape function</param>
	/// <param name="schemaSeparator">Schema separator</param>
	string GetFullName(Func<string, string> escape, char schemaSeparator);
}
