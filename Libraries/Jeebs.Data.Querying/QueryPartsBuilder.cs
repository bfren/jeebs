using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryPartsBuilder{TModel, TOptions}"/>
	public abstract class QueryPartsBuilder<TModel, TOptions> : IQueryPartsBuilder<TModel, TOptions>
		where TOptions : IQueryOptions
	{
		/// <inheritdoc/>
		public IAdapter Adapter { get; }

		/// <summary>
		/// QueryParts
		/// </summary>
		internal IQueryParts Parts { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="adapter">IAdapter</param>
		/// <param name="from">FROM command</param>
		protected QueryPartsBuilder(IAdapter adapter, string from)
			=> (Adapter, Parts) = (adapter, new QueryParts(from));

		/// <summary>
		/// Build the query
		/// </summary>
		/// <param name="opt">TOptions</param>
		public abstract IQueryParts Build(TOptions opt);

		/// <summary>
		/// Finish Build process by adding ORDER BY, LIMIT and OFFSET values
		/// </summary>
		/// <param name="opt">TOptions</param>
		/// <param name="defaultSort">Default sort columns</param>
		protected IQueryParts FinishBuild(TOptions opt, params (string column, SortOrder order)[] defaultSort)
		{
			// ORDER BY
			AddSort(opt, defaultSort);

			// LIMIT and OFFSET
			AddLimitAndOffset(opt);

			// Return
			return Parts;
		}

		/// <summary>
		/// Add Sort
		/// </summary>
		/// <param name="opt">TOptions</param>
		/// <param name="defaultSort">Default sort columns</param>
		internal void AddSort(TOptions opt, params (string column, SortOrder order)[] defaultSort)
		{
			// Random sort
			if (opt.SortRandom)
			{
				Parts.OrderBy = new List<string> { Adapter.GetRandomSortOrder() };
			}
			// Specified sort
			else if (opt.Sort is (string column, SortOrder order)[] sort && sort.Length > 0)
			{
				Add(sort);
			}
			// Default sort
			else
			{
				Add(defaultSort);
			}

			// Add to ORDER BY
			void Add(params (string column, SortOrder order)[] sort)
			{
				if (sort.Length == 0)
				{
					return;
				}

				Parts.OrderBy ??= new List<string>();
				foreach (var (column, order) in sort)
				{
					Parts.OrderBy.Add(Adapter.GetSortOrder(column, order));
				}
			}
		}

		/// <summary>
		/// Add Limit and Offset
		/// </summary>
		/// <param name="opt">TOptions</param>
		internal void AddLimitAndOffset(TOptions opt)
		{
			// LIMIT
			Parts.Limit = opt.Limit;

			// OFFSET
			Parts.Offset = opt.Offset;
		}

		/// <summary>
		/// Set SELECT
		/// </summary>
		/// <param name="select">SELECT string</param>
		/// <param name="overwrite">[Optional] If true, will overwrite whatever already exists in SELECT</param>
		protected void AddSelect(string select, bool overwrite = false)
		{
			if (string.IsNullOrWhiteSpace(Parts.Select) || overwrite)
			{
				Parts.Select = select;
			}
			else
			{
				throw new Jx.Data.Querying.SelectAlreadySetException();
			}
		}

		/// <summary>
		/// Add JOIN
		/// </summary>
		/// <param name="join">JOINT list</param>
		/// <param name="table">JOIN table</param>
		/// <param name="on">JOIN column - should be a column on the JOIN table</param>
		/// <param name="equals">EQUALS table and column</param>
		/// <param name="escape">Whether or not to escape table and column names</param>
		internal IList<(string table, string on, string equals)> AddJoin(
			IList<(string table, string on, string equals)>? join,
			object table,
			string on,
			(object table, string column) equals,
			bool escape
		)
		{
			// Use existing list or create new one
			var joinList = join ?? new List<(string table, string on, string equals)>();

			// Add the join
			if (escape)
			{
				joinList.Add((
					Adapter.EscapeTable(table),
					Adapter.EscapeAndJoin(table, on),
					Adapter.EscapeAndJoin(equals.table, equals.column)
				));
			}
			else
			{
				joinList.Add((
					table.ToString(),
					Adapter.Join(table, on),
					Adapter.Join(equals.table, equals.column)
				));
			}

			// Return the join list
			return joinList;
		}

		/// <summary>
		/// Set INNER JOIN
		/// </summary>
		/// <param name="table">JOIN table</param>
		/// <param name="on">JOIN column - should be a column on the JOIN table</param>
		/// <param name="equals">EQUALS table and column</param>
		/// <param name="escape">[Optional] Set to true to enable automatic escaping of JOIN statement</param>
		protected void AddInnerJoin(object table, string on, (object table, string column) equals, bool escape = false)
			=> Parts.InnerJoin = AddJoin(Parts.InnerJoin, table, on, equals, escape);

		/// <summary>
		/// Set LEFT JOIN
		/// </summary>
		/// <param name="table">JOIN table</param>
		/// <param name="on">JOIN column - should be a column on the JOIN table</param>
		/// <param name="equals">EQUALS table and column</param>
		/// <param name="escape">[Optional] Set to true to enable automatic escaping of JOIN statement</param>
		protected void AddLeftJoin(object table, string on, (object table, string column) equals, bool escape = false)
			=> Parts.LeftJoin = AddJoin(Parts.LeftJoin, table, on, equals, escape);

		/// <summary>
		/// Set RIGHT JOIN
		/// </summary>
		/// <param name="table">JOIN table</param>
		/// <param name="on">JOIN column - should be a column on the JOIN table</param>
		/// <param name="equals">EQUALS table and column</param>
		/// <param name="escape">[Optional] Set to true to enable automatic escaping of JOIN statement</param>
		protected void AddRightJoin(object table, string on, (object table, string column) equals, bool escape = false)
			=> Parts.RightJoin = AddJoin(Parts.RightJoin, table, on, equals, escape);

		/// <summary>
		/// Add WHERE clause
		/// </summary>
		/// <param name="where">WHERE string</param>
		/// <param name="parameters">[Optional] Parameters to add</param>
		protected void AddWhere(string where, object? parameters = null)
		{
			(Parts.Where ??= new List<string>()).Add(where);

			if (parameters != null)
			{
				Parts.Parameters.Add(parameters);
			}
		}
	}
}
