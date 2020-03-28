using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;

namespace Jeebs.Data
{
	/// <summary>
	/// Query Builder
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TOptions">QueryOptions</typeparam>
	public abstract class QueryBuilder<TEntity, TOptions>
		where TOptions : QueryOptions
	{
		/// <summary>
		/// QueryArgs
		/// </summary>
		protected QueryArgs<TEntity> Args { get; }

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
			Args = new QueryArgs<TEntity>();
			this.adapter = adapter;
		}

		/// <summary>
		/// Build the query
		/// </summary>
		/// <param name="opt">TOptions</param>
		/// <returns>QueryArgs</returns>
		public abstract QueryArgs<TEntity> Build(TOptions opt);

		/// <summary>
		/// Add Sort
		/// </summary>
		/// <param name="opt">TOptions</param>
		/// <param name="defaultSort">Default sort columns</param>
		protected void AddSort(TOptions opt, (string selectColumn, SortOrder order)[] defaultSort)
		{
			// Random sort
			if (opt.SortRandom)
			{
				(Args.OrderBy ?? (Args.OrderBy = new List<string>())).Clear();
				Args.OrderBy.Add(adapter.GetRandomSortOrder());
			}
			// Specified sort
			else if (opt.Sort is (string selectColumn, SortOrder order)[] sort)
			{
				Add(sort);
			}
			// Default sort
			else
			{
				Add(defaultSort);
			}

			// Add to ORDER BY
			void Add(params (string selectColumn, SortOrder order)[] sort)
			{
				if (sort.Length == 0)
				{
					return;
				}

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
		/// Add Limit and Offset
		/// </summary>
		/// <param name="opt">TOptions</param>
		protected void AddLimitAndOffset(TOptions opt)
		{
			// LIMIT
			if (opt.Limit is long limit)
			{
				Args.Limit = limit;
			}

			// OFFSET
			if (opt.Offset is long offset)
			{
				Args.Offset = offset;
			}
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
		/// Add JOIN
		/// </summary>
		/// <param name="join">JOINT list</param>
		/// <param name="table">Table to join</param>
		/// <param name="on">Table and column to join from</param>
		/// <param name="equals">Table and column to join to</param>
		private List<(string table, string on, string equals)> AddJoin(
			List<(string table, string on, string equals)>? join,
			string table,
			string on,
			(string table, string column) equals
		)
		{
			// Use existing list or create new one
			var joinList = join ?? new List<(string table, string on, string equals)>();

			// Add the join
			joinList.Add((
				Escape(table),
				Escape(table, on),
				Escape(equals.table, equals.column)
			));

			// Return the join list
			return joinList;
		}

		/// <summary>
		/// Set INNER JOIN
		/// </summary>
		protected void AddInnerJoin(string table, string on, (string table, string column) equals)
			=> Args.InnerJoin = AddJoin(Args.InnerJoin, table, on, equals);

		/// <summary>
		/// Set INNER JOIN
		/// </summary>
		protected void AddLeftJoin(string table, string on, (string table, string column) equals)
			=> Args.LeftJoin = AddJoin(Args.LeftJoin, table, on, equals);

		/// <summary>
		/// Set INNER JOIN
		/// </summary>
		protected void AddRightJoin(string table, string on, (string table, string column) equals)
			=> Args.RightJoin = AddJoin(Args.RightJoin, table, on, equals);

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
	}
}
