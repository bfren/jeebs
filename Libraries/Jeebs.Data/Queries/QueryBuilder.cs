using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <summary>
	/// Query Builder
	/// </summary>
	/// <typeparam name="TOptions">QueryOptions</typeparam>
	public abstract class QueryBuilder<TOptions>
		where TOptions : QueryOptions
	{
		/// <summary>
		/// QueryArgs
		/// </summary>
		protected QueryArgs Args { get; }

		/// <summary>
		/// IAdapter
		/// </summary>
		private readonly IAdapter adapter;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="adapter">IAdapter</param>
		protected QueryBuilder(IAdapter adapter)
		{
			Args = new QueryArgs();
			this.adapter = adapter;
		}

		/// <summary>
		/// Build the query
		/// </summary>
		/// <typeparam name="T">Entity type to return</typeparam>
		/// <param name="opt">TOptions</param>
		/// <returns>QueryArgs</returns>
		public virtual QueryArgs Build<T>(TOptions opt)
		{
			// LIMIT
			if (opt.Limit is double limit)
			{
				AddLimit(limit);
			}

			// OFFSET
			if (opt.Offset is double offset)
			{
				AddOffset(offset);
			}

			return Args;
		}

		#region Adapter Shorthands

		/// <summary>
		/// Shorthand for Table[].ExtractColumns and then IAdapter.Join
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="tables">List of tables from which to extract columns that match <typeparamref name="T"/></param>
		public string Extract<T>(params Table[] tables) => adapter.Join(tables.ExtractColumns<T>());

		/// <summary>
		/// Shorthand for IAdapter.SplitAndEscape
		/// </summary>
		/// <param name="element">The element to split and escape</param>
		public string Escape(object element) => adapter.SplitAndEscape(element.ToString());

		/// <summary>
		/// Shorthand for IAdapter.EscapeAndJoin
		/// </summary>
		/// <param name="elements">The elements to escape and join</param>
		public string Escape(params string?[] elements) => adapter.EscapeAndJoin(elements);

		#endregion

		/// <summary>
		/// Set FROM
		/// </summary>
		/// <param name="from">FROM string</param>
		/// <param name="overwrite">[Optional] If true, will overwrite whatever already exists in FROM</param>
		protected void AddFrom(string from, bool overwrite = false)
		{
			if (string.IsNullOrEmpty(Args.From) || overwrite)
			{
				Args.From = from;
			}
			else
			{
				throw new Jx.Data.QueryException("FROM has already been set.");
			}
		}

		/// <summary>
		/// Set SELECT
		/// </summary>
		/// <param name="select">SELECT string</param>
		/// <param name="overwrite">[Optional] If true, will overwrite whatever already exists in SELECT</param>
		protected void AddSelect(string select, bool overwrite = false)
		{
			if (string.IsNullOrEmpty(Args.Select) || overwrite)
			{
				Args.Select = select;
			}
			else
			{
				throw new Jx.Data.QueryException("SELECT has already been set.");
			}
		}

		/// <summary>
		/// Add WHERE clause
		/// </summary>
		/// <param name="where">WHERE string</param>
		protected void AddWhere(string where, object? parameters = null)
		{
			(Args.Where ?? (Args.Where = new List<string>())).Add(where);

			if (parameters != null)
			{
				Args.Parameters.Add(parameters);
			}
		}

		/// <summary>
		/// Add ORDER BY random - will clear previous order by items
		/// </summary>
		protected void AddOrderByRandom()
		{
			(Args.OrderBy ?? (Args.OrderBy = new List<string>())).Clear();
			Args.OrderBy.Add(adapter.GetRandomSortOrder());
		}

		/// <summary>
		/// Add ORDER BY columns
		/// </summary>
		/// <param name="sort">Sort columns</param>
		protected void AddOrderBy(params (string selectColumn, SortOrder order)[] sort)
		{
			// Add sort clauses
			if (sort.Length > 0)
			{
				if (Args.OrderBy == null)
				{
					Args.OrderBy = new List<string>();
				}

				foreach (var (column, order) in sort)
				{
					Args.OrderBy.Add(adapter.GetSortOrder(column, order));
				}
			}
		}

		/// <summary>
		/// Add LIMIT
		/// </summary>
		/// <param name="limit"></param>
		protected void AddLimit(double limit) => Args.Limit = limit;

		/// <summary>
		/// Add OFFSET
		/// </summary>
		protected void AddOffset(double offset) => Args.Offset = offset;
	}
}
