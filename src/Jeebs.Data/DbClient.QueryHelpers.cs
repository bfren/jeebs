// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.Data
{
	public abstract partial class DbClient : IDbClient
	{
		/// <summary>
		/// Get columns and parameter names for <see cref="GetCreateQuery(string, IMappedColumnList)"/>
		/// </summary>
		/// <param name="columns">IMappedColumnList</param>
		protected virtual (List<string> col, List<string> par) GetColumnsForCreateQuery(IMappedColumnList columns)
		{
			var col = new List<string>();
			var par = new List<string>();
			foreach (var column in columns)
			{
				col.Add(Escape(column.Name));
				par.Add(GetParamRef(column.Alias));
			}

			return (col, par);
		}

		/// <summary>
		/// Get columns for <see cref="GetRetrieveQuery(string, ColumnList, IColumn, long)"/>
		/// </summary>
		/// <param name="columns">ColumnList</param>
		protected virtual List<string> GetColumnsForRetrieveQuery(ColumnList columns)
		{
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add(Escape(column, true));
			}

			return col;
		}

		/// <summary>
		/// Get columns for <see cref="GetUpdateQuery(string, ColumnList, IColumn, long, IColumn?)"/>
		/// </summary>
		/// <param name="columns">ColumnList</param>
		protected virtual List<string> GetColumnsForUpdateQuery(ColumnList columns)
		{
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add($"{Escape(column)} = {GetParamRef(column.Alias)}");
			}

			return col;
		}

		/// <summary>
		/// Add version to column list for <see cref="GetUpdateQuery(string, ColumnList, IColumn, long, IColumn?)"/>,
		/// if <paramref name="versionColumn"/> is not null
		/// </summary>
		/// <param name="columns">List of column names</param>
		/// <param name="versionColumn">[Optional] Version column</param>
		protected virtual void AddVersionToColumnList(List<string> columns, IColumn? versionColumn)
		{
			if (versionColumn is not null)
			{
				columns.Add($"{Escape(versionColumn)} = {GetParamRef(versionColumn.Alias)} + 1");
			}
		}

		/// <summary>
		/// Add version to where string for <see cref="GetUpdateQuery(string, ColumnList, IColumn, long, IColumn?)"/>
		/// and <see cref="GetDeleteQuery(string, IColumn, long, IColumn?)"/>
		/// </summary>
		/// <param name="sql">SQL query StringBuilder</param>
		/// <param name="versionColumn">[Optional] Version column</param>
		protected virtual void AddVersionToWhere(StringBuilder sql, IColumn? versionColumn)
		{
			if (versionColumn is not null)
			{
				sql.Append($" AND {Escape(versionColumn)} = {GetParamRef(versionColumn.Alias)}");
			}
		}
	}
}
