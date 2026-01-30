// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;

namespace Jeebs.Data;

/// <summary>
/// Holds information about a mapped column.
/// </summary>
public interface IColumn
{
	/// <summary>
	/// Table Name.
	/// </summary>
	IDbName TblName { get; }

	/// <summary>
	/// Column Name.
	/// </summary>
	string ColName { get; }

	/// <summary>
	/// Column Alias.
	/// </summary>
	string ColAlias { get; }

	/// <summary>
	/// Entity Property.
	/// </summary>
	PropertyInfo PropertyInfo { get; }
}
