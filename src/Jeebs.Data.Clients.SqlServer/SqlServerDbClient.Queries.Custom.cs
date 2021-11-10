﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using static F.DataF.QueryF;

namespace Jeebs.Data.Clients.SqlServer;

public partial class SqlServerDbClient : DbClient
{
	/// <inheritdoc/>
	protected override (string query, IQueryParameters param) GetQuery(
		ITableName table,
		IColumnList columns,
		IImmutableList<(IColumn column, Compare cmp, object value)> predicates
	)
	{
		// Get columns
		var col = GetColumnsFromList(this, columns);

		// Add each predicate to the where and parameter lists
		var (where, param) = GetWhereAndParameters(this, predicates, false);

		// Return query and parameters
		return (
			$"SELECT {JoinList(col, false)} FROM {Escape(table)} WHERE {string.Join(" AND ", where)}",
			param
		);
	}

	/// <inheritdoc/>
	public override (string query, IQueryParameters param) GetQuery(IQueryParts parts)
	{
		// Start query
		var select = parts.SelectCount switch
		{
			true =>
				"COUNT(*)",

			false =>
				(parts.Select.Count > 0) switch
				{
					true =>
						GetSelectFromList(this, parts.Select),

					false =>
						"*"
				}
		};

		var sql = new StringBuilder($"SELECT {select} FROM {Escape(parts.From)}");

		// Add INNER JOIN
		foreach (var (from, to) in parts.InnerJoin)
		{
			sql.Append($" INNER JOIN {Escape(to.Table)} ON {Escape(from.Table, from.Name)} = {Escape(to.Table, to.Name)}");
		}

		// Add LEFT JOIN
		foreach (var (from, to) in parts.LeftJoin)
		{
			sql.Append($" LEFT JOIN {Escape(to.Table)} ON {Escape(from.Table, from.Name)} = {Escape(to.Table, to.Name)}");
		}

		// Add RIGHT JOIN
		foreach (var (from, to) in parts.RightJoin)
		{
			sql.Append($" RIGHT JOIN {Escape(to.Table)} ON {Escape(from.Table, from.Name)} = {Escape(to.Table, to.Name)}");
		}

		// Add WHERE
		IQueryParameters parameters = new QueryParameters();
		if (parts.Where.Count > 0 || parts.WhereCustom.Count > 0)
		{
			// This will be appended to the SQL query
			var where = new List<string>();

			// Add simple WHERE
			if (parts.Where.Count > 0)
			{
				var (whereSimple, param) = GetWhereAndParameters(this, parts.Where, true);
				where.AddRange(whereSimple);
				parameters.Merge(param);
			}

			// Add custom WHERE
			foreach (var (whereCustom, param) in parts.WhereCustom)
			{
				where.Add($"({whereCustom})");
				parameters.Merge(param);
			}

			// If there's anything to add, 
			if (where.Count > 0)
			{
				sql.Append($" WHERE {string.Join(" AND ", where)}");
			}
		}

		// Add ORDER BY
		if (parts.SortRandom)
		{
			sql.Append(" ORDER BY NEWID()");
		}
		else if (parts.Sort.Count > 0)
		{
			var orderBy = new List<string>();
			foreach (var (column, order) in parts.Sort)
			{
				orderBy.Add($"{Escape(column.Table, column.Name)} {order.ToOperator()}");
			}

			sql.Append($" ORDER BY {JoinList(orderBy, false)}");
		}

		// Add OFFSET and FETCH
		if (parts.Maximum > 0)
		{
			sql.Append($" OFFSET {parts.Skip} ROWS");
			sql.Append($" FETCH NEXT {parts.Maximum} ROWS ONLY");
		}

		// Return query string
		return (
			sql.ToString(),
			parameters
		);
	}
}
