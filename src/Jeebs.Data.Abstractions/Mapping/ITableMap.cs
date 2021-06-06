// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Table Map
	/// </summary>
	public interface ITableMap
	{
		/// <summary>
		/// Table object
		/// </summary>
		ITable Table { get; init; }

		/// <summary>
		/// Table Name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Mapped Columns
		/// </summary>
		IMappedColumnList Columns { get; init; }

		/// <summary>
		/// Id Column
		/// </summary>
		IMappedColumn IdColumn { get; init; }

		/// <summary>
		/// [Optional] Version Column
		/// </summary>
		IMappedColumn? VersionColumn { get; }

		/// <summary>
		/// Get all column names
		/// </summary>
		IEnumerable<string> GetColumnNames();

		/// <summary>
		/// Get all column aliases
		/// </summary>
		/// <param name="includeIdAlias">If true, the ID column alias will be included</param>
		IEnumerable<string> GetColumnAliases(bool includeIdAlias);

		/// <summary>
		/// Get all column names and aliases for writeable columns
		/// (i.e. not marked as Id / Computed / Readonly)
		/// </summary>
		Option<(List<string> names, List<string> aliases)> GetWriteableColumnNamesAndAliases();
	}
}