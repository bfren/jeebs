// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Collections;
using Jeebs.Data.Common.FluentQuery;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query;

namespace Jeebs.Data.Clients.SqlServer;

public partial class SqlServerDbClient
{
	/// <inheritdoc/>
	protected override Result<(string query, IQueryParametersDictionary param)> GetQuery(
		ITableName table,
		IColumnList columns,
		IImmutableList<(IColumn column, Compare cmp, dynamic value)> predicates
	)
	{
		// Get columns
		var col = QueryF.GetColumnsFromList(this, columns);

		// Add each predicate to the where and parameter lists
		var (where, param) = QueryF.GetWhereAndParameters(this, predicates, false);

		// Return query and parameters
		return (
			$"SELECT {JoinList(col, false)} FROM {Escape(table)} WHERE {string.Join(" AND ", where)}",
			param
		);
	}

	/// <inheritdoc/>
	public override Result<(string query, IQueryParametersDictionary param)> GetQuery(IQueryParts parts)
	{
		// Start query
		var select = parts.SelectCount switch
		{
			true =>
				"COUNT(*)",

			false =>
				(parts.SelectColumns.Count > 0) switch
				{
					true =>
						QueryF.GetSelectFromList(this, parts.SelectColumns),

					false =>
						"*"
				}
		};

		var sql = $"SELECT {select} FROM {Escape(parts.From)}";

		// Add INNER JOIN
		foreach (var (from, to) in parts.InnerJoin)
		{
			sql += $" INNER JOIN {Escape(to.TblName)} ON {Escape(from.TblName, from.ColName)} = {Escape(to.TblName, to.ColName)}";
		}

		// Add LEFT JOIN
		foreach (var (from, to) in parts.LeftJoin)
		{
			sql += $" LEFT JOIN {Escape(to.TblName)} ON {Escape(from.TblName, from.ColName)} = {Escape(to.TblName, to.ColName)}";
		}

		// Add RIGHT JOIN
		foreach (var (from, to) in parts.RightJoin)
		{
			sql += $" RIGHT JOIN {Escape(to.TblName)} ON {Escape(from.TblName, from.ColName)} = {Escape(to.TblName, to.ColName)}";
		}

		// Add WHERE
		var parameters = new QueryParametersDictionary();
		if (parts.Where.Count > 0 || parts.WhereCustom.Count > 0)
		{
			// This will be appended to the SQL query
			var where = new List<string>();

			// Add simple WHERE
			if (parts.Where.Count > 0)
			{
				var (whereSimple, param) = QueryF.GetWhereAndParameters(this, parts.Where, true);
				where.AddRange(whereSimple);
				_ = parameters.Merge(param);
			}

			// Add custom WHERE
			foreach (var (whereCustom, param) in parts.WhereCustom)
			{
				where.Add($"({whereCustom})");
				_ = parameters.Merge(param);
			}

			// If there are any WHERE clauses, add them to the SQL string
			if (where.Count > 0)
			{
				sql += $" WHERE {string.Join(" AND ", where)}";
			}
		}

		// Add ORDER BY
		if (parts.SortRandom)
		{
			sql += " ORDER BY NEWID()";
		}
		else if (parts.Sort.Count > 0)
		{
			var orderBy = new List<string>();
			foreach (var (column, order) in parts.Sort)
			{
				orderBy.Add($"{Escape(column.TblName, column.ColName)} {order.ToOperator()}");
			}

			sql += $" ORDER BY {JoinList(orderBy, false)}";
		}

		// Add OFFSET and FETCH
		if (parts.Maximum > 0)
		{
			sql += $" OFFSET {parts.Skip} ROWS";
			sql += $" FETCH NEXT {parts.Maximum} ROWS ONLY";
		}

		// Return query string
		return (sql, parameters);
	}
}
