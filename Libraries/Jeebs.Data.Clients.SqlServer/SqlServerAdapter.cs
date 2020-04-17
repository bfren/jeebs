using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Clients.SqlServer
{
	/// <summary>
	/// SqlServer adapter
	/// </summary>
	public sealed class SqlServerAdapter : Adapter
	{
		/// <summary>
		/// Create object
		/// </summary>
		public SqlServerAdapter() : base('.', '[', ']', "AS", '[', ']', "ASC", "DESC") { }

		/// <summary>
		/// Return random sort string
		/// </summary>
		public override string GetRandomSortOrder() => "NEWID()";

		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">Columns (actual column names in database)</param>
		/// <param name="aliases">Aliases (parameter names / POCO property names)</param>
		public override string CreateSingleAndReturnId(string table, List<string> columns, List<string> aliases)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Build a SELECT query
		/// </summary>
		/// <param name="parts">IQueryParts</param>
		/// <returns>SELECT query</returns>
		public override string Retrieve(IQueryParts parts)
		{
			// Start query
			StringBuilder sql = new StringBuilder($"SELECT {parts.Select ?? "*"} FROM {parts.From}");

			// Add INNER JOIN
			if (parts.InnerJoin is List<(string table, string on, string equals)> innerJoinValues)
			{
				foreach (var item in innerJoinValues)
				{
					sql.Append($" INNER JOIN {item.table} ON {item.on} = {item.equals}");
				}
			}

			// Add LEFT JOIN
			if (parts.LeftJoin is List<(string table, string on, string equals)> leftJoinValues)
			{
				foreach (var item in leftJoinValues)
				{
					sql.Append($" LEFT JOIN {item.table} ON {item.on} = {item.equals}");
				}
			}

			// Add RIGHT JOIN
			if (parts.RightJoin is List<(string table, string on, string equals)> rightJoinValues)
			{
				foreach (var item in rightJoinValues)
				{
					sql.Append($" RIGHT JOIN {item.table} ON {item.on} = {item.equals}");
				}
			}

			// Add WHERE
			if (parts.Where is List<string> whereValue)
			{
				sql.Append($" WHERE {string.Join(" AND ", whereValue)}");
			}

			// Add ORDER BY
			if (parts.OrderBy is List<string> orderByValue)
			{
				sql.Append($" ORDER BY {string.Join(", ", orderByValue)}");
			}

			// Add LIMIT
			if (parts.Limit is long limitValue && limitValue > 0)
			{
				sql.Append($" LIMIT {limitValue}");
			}

			// Add OFFSET
			if (parts.Offset is long offsetValue && offsetValue > 0)
			{
				sql.Append($" OFFSET {offsetValue}");
			}

			// Return query string
			return sql.ToString();
		}

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <param name="columns">The columns to SELECT</param>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column</param>
		public override string RetrieveSingleById(List<string> columns, string table, string idColumn)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">Columns (actual column names in database)</param>
		/// <param name="aliases">Aliases (parameter names / POCO property names)</param>
		/// <param name="idColumn">ID column (actual column name in database)</param>
		/// <param name="idAlias">ID alias (parameter name / POCO property name)</param>
		/// <param name="versionColumn">[Optional] Version column (actual column name in database)</param>
		/// <param name="versionAlias">[Optional] Version alias (parameter name / POCO property name)</param>
		public override string UpdateSingle(string table, List<string> columns, List<string> aliases, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column (actual column name in database)</param>
		/// <param name="idAlias">ID alias (parameter name / POCO property name)</param>
		/// <param name="versionColumn">[Optional] Version column (actual column name in database)</param>
		/// <param name="versionAlias">[Optional] Version alias (parameter name / POCO property name)</param>
		public override string DeleteSingle(string table, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null)
		{
			throw new NotImplementedException();
		}
	}
}