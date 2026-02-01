// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs.Data.Map;

/// <summary>
/// Table Map.
/// </summary>
public interface ITableMap
{
	/// <summary>
	/// Table object.
	/// </summary>
	ITable Table { get; init; }

	/// <summary>
	/// Table Name.
	/// </summary>
	ITableName Name { get; }

	/// <summary>
	/// Mapped Columns.
	/// </summary>
	IColumnList Columns { get; init; }

	/// <summary>
	/// Id Column.
	/// </summary>
	IColumn IdColumn { get; init; }

	/// <summary>
	/// [Optional] Version Column.
	/// </summary>
	IColumn? VersionColumn { get; }

	/// <summary>
	/// Get all column names.
	/// </summary>
	IEnumerable<string> GetColumnNames();

	/// <summary>
	/// Get all column aliases.
	/// </summary>
	/// <param name="includeIdAlias">If true, the ID column alias will be included.</param>
	IEnumerable<string> GetColumnAliases(bool includeIdAlias);

	/// <summary>
	/// Get all column names and aliases for writeable columns
	/// (i.e. not marked as Id / Computed / Readonly)
	/// </summary>
	Result<(List<string> names, List<string> aliases)> GetWriteableColumnNamesAndAliases();
}
