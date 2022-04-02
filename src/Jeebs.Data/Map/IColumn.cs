// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map;

/// <summary>
/// Holds information about a column
/// </summary>
public interface IColumn
{
	/// <summary>
	/// Table Name
	/// </summary>
	ITableName TblName { get; }

	/// <summary>
	/// Column Name
	/// </summary>
	string ColName { get; }

	/// <summary>
	/// Column Alias
	/// </summary>
	string ColAlias { get; }
}
