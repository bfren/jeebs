// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.Data;

public abstract partial class DbClient : IDbClient
{
	/// <summary>
	/// Get columns and parameter names for <see cref="GetCreateQuery(ITableName, IMappedColumnList)"/>
	/// </summary>
	/// <param name="columns">IMappedColumnList</param>
	protected virtual (List<string> col, List<string> par) GetColumnsForCreateQuery(IMappedColumnList columns)
	{
		var col = new List<string>();
		var par = new List<string>();
		foreach (var column in columns)
		{
			if (column.Property.IsReadonly())
			{
				continue;
			}

			col.Add(Escape(column.Name));
			par.Add(GetParamRef(column.Alias));
		}

		return (col, par);
	}

	/// <summary>
	/// Get columns for <see cref="GetRetrieveQuery(ITableName, IColumnList, IColumn, long)"/>
	/// </summary>
	/// <param name="columns">ColumnList</param>
	protected virtual List<string> GetColumnsForRetrieveQuery(IColumnList columns)
	{
		var col = new List<string>();
		foreach (var column in columns)
		{
			col.Add(Escape(column, true));
		}

		return col;
	}

	/// <summary>
	/// Get columns for <see cref="GetUpdateQuery(ITableName, IColumnList, IColumn, long, IColumn?)"/>
	/// </summary>
	/// <param name="columns">ColumnList</param>
	protected virtual List<string> GetSetListForUpdateQuery(IColumnList columns)
	{
		var col = new List<string>();
		foreach (var column in columns)
		{
			col.Add($"{Escape(column)} = {GetParamRef(column.Alias)}");
		}

		return col;
	}

	/// <summary>
	/// Add version to column list for <see cref="GetUpdateQuery(ITableName, IColumnList, IColumn, long, IColumn?)"/>,
	/// if <paramref name="versionColumn"/> is not null
	/// </summary>
	/// <param name="set">List of Set commands</param>
	/// <param name="versionColumn">[Optional] Version column</param>
	protected virtual void AddVersionToSetList(List<string> set, IColumn? versionColumn)
	{
		if (versionColumn is not null)
		{
			set.Add($"{Escape(versionColumn)} = {GetParamRef(versionColumn.Alias)} + 1");
		}
	}

	/// <summary>
	/// Add version to where string for <see cref="GetUpdateQuery(ITableName, IColumnList, IColumn, long, IColumn?)"/>
	/// and <see cref="GetDeleteQuery(ITableName, IColumn, long, IColumn?)"/>
	/// </summary>
	/// <param name="sql">SQL query StringBuilder</param>
	/// <param name="versionColumn">[Optional] Version column</param>
	protected virtual void AddVersionToWhere(StringBuilder sql, IColumn? versionColumn)
	{
		if (versionColumn is not null)
		{
			if (sql.Length > 0)
			{
				sql.Append(" AND ");
			}

			sql.Append($"{Escape(versionColumn)} = {GetParamRef(versionColumn.Alias)}");
		}
	}

	#region Testing

	internal (List<string> col, List<string> par) GetColumnsForCreateQueryTest(IMappedColumnList columns) =>
		GetColumnsForCreateQuery(columns);

	internal List<string> GetColumnsForRetrieveQueryTest(IColumnList columns) =>
		GetColumnsForRetrieveQuery(columns);

	internal List<string> GetSetListForUpdateQueryTest(IColumnList columns) =>
		GetSetListForUpdateQuery(columns);

	internal void AddVersionToSetListTest(List<string> columns, IColumn? versionColumn) =>
		AddVersionToSetList(columns, versionColumn);

	internal void AddVersionToWhereTest(StringBuilder sql, IColumn? versionColumn) =>
		AddVersionToWhere(sql, versionColumn);

	#endregion
}
