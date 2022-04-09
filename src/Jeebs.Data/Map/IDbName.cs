// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.Map;

/// <summary>
/// Database object name and optional schema
/// </summary>
public interface IDbName
{
	/// <summary>
	/// [Optional] Object Schema
	/// </summary>
	string? Schema { get; }

	/// <summary>
	/// Object Name
	/// </summary>
	string Name { get; }

	/// <summary>
	/// Escape and return object <see cref="Schema"/> and <see cref="Name"/> separated by '.',
	/// or simply the <see cref="Name"/> if <see cref="Schema"/> is null
	/// </summary>
	/// <param name="escape">Escape function</param>
	string GetFullName(Func<string, string> escape);

	/// <summary>
	/// Escape and return object <see cref="Schema"/> and <see cref="Name"/> separated by '.',
	/// or simply the <see cref="Name"/> if <see cref="Schema"/> is null
	/// </summary>
	/// <param name="escape">Escape function</param>
	/// <param name="schemaSeparator">Schema separator</param>
	string GetFullName(Func<string, string> escape, char schemaSeparator);
}
