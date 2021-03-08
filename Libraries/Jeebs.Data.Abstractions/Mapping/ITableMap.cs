﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Table Map
	/// </summary>
	public interface ITableMap
	{
		/// <summary>
		/// Table Name
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Mapped Columns
		/// </summary>
		IMappedColumnList Columns { get; set; }

		/// <summary>
		/// Id Column
		/// </summary>
		IMappedColumn IdColumn { get; set; }

		/// <summary>
		/// [Optional] Version Column
		/// </summary>
		IMappedColumn? VersionColumn { get; set; }

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
		/// (i.e. not marked with <see cref="IdAttribute"/>, <see cref="ComputedAttribute"/> or <see cref="ReadonlyAttribute"/>
		/// </summary>
		(List<string> names, List<string> aliases) GetWriteableColumnNamesAndAliases();

		/// <summary>
		/// Should be overridden to provide the table name (escaped)
		/// </summary>
		string ToString();
	}
}