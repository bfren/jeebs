// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Functions;
using Jeebs.Data.Map;
using Jeebs.Data.Query;

namespace Jeebs.Data;

public abstract partial class DbClient : IDbClient
{
	/// <summary>
	/// SQL expression used to sort results randomly, e.g. 'RAND()'.
	/// </summary>
	protected abstract string RandomSort { get; }

	/// <summary>
	/// Appended to the end of every custom query.
	/// </summary>
	protected virtual string QueryTerminator =>
		";";

	/// <summary>
	/// Return the clause that limits and offsets results (with a leading space), or an empty string.
	/// </summary>
	/// <param name="maximum">Maximum number of results to return (null or 0 for no limit).</param>
	/// <param name="skip">Number of results to skip (0 for none).</param>
	/// <param name="sorted">Whether the query already has an ORDER BY clause.</param>
	protected abstract string GetLimitAndOffset(ulong? maximum, ulong skip, bool sorted);

	/// <inheritdoc/>
	public Result<(string query, IQueryParametersDictionary param)> GetCountQuery(IQueryParts parts) =>
		// a count returns one row, so sorting and paging it would fail or skip the count itself
		GetQuery(new QueryParts(parts) with
		{
			SelectCount = true,
			Sort = new ImmutableList<(IColumn column, SortOrder order)>(),
			SortRandom = false,
			Maximum = null,
			Skip = 0
		});

	/// <inheritdoc/>
	public virtual Result<(string query, IQueryParametersDictionary param)> GetQuery(IQueryParts parts)
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
						DataF.GetSelectFromList(this, parts.SelectColumns),

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
				var (whereSimple, param) = DataF.GetWhereAndParameters(this, parts.Where, true);
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
			sql += $" ORDER BY {RandomSort}";
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

		// Add LIMIT and OFFSET
		sql += GetLimitAndOffset(parts.Maximum, parts.Skip, parts.SortRandom || parts.Sort.Count > 0);

		// Return query string
		return ($"{sql}{QueryTerminator}", parameters);
	}
}
