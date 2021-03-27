// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Exceptions;
using Jeebs.Linq;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDbClient"/>
	public abstract class DbClient : IDbClient
	{
		/// <inheritdoc/>
		public abstract IDbConnection Connect(string connectionString);

		#region Escaping and Joining

		/// <summary>
		/// Escape a column
		/// </summary>
		/// <param name="column">Column</param>
		protected abstract string Escape(IColumn column);

		/// <summary>
		/// Escape a table
		/// </summary>
		/// <param name="table">Table</param>
		protected abstract string Escape(ITable table);

		/// <summary>
		/// Escape a column or table
		/// </summary>
		/// <param name="columnOrTable">Column or Table name</param>
		protected abstract string Escape(string columnOrTable);

		/// <summary>
		/// Escape a column with its table
		/// </summary>
		/// <param name="column">Column name</param>
		/// <param name="table">Table name</param>
		protected abstract string Escape(string column, string table);

		/// <summary>
		/// Convert a <see cref="SearchOperator"/> to actual operator
		/// </summary>
		/// <param name="op">SearchOperator</param>
		protected abstract string GetOperator(SearchOperator op);

		/// <summary>
		/// Join a list of columns or parameters to be used in a query, e.g. to (@P0,@P1,@P2)
		/// </summary>
		/// <param name="objects">Objects to join</param>
		/// <param name="wrap">If true, the list will be wrapped (usually in parentheses)</param>
		protected abstract string JoinList(List<string> objects, bool wrap);

		#endregion

		#region Custom Queries

		/// <summary>
		/// Return a query to retrieve a list of entities that match all the specified parameters
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="predicates">Predicates (matched using AND)</param>
		protected abstract (string query, IQueryParameters param) GetQuery(
			string table,
			ColumnList columns,
			List<(IColumn column, SearchOperator op, object value)> predicates
		);

		/// <inheritdoc/>
		public Option<(string query, IQueryParameters param)> GetQuery<TEntity, TModel>(
			(Expression<Func<TEntity, object>>, SearchOperator, object)[] predicates
		)
			where TEntity : IEntity =>
			(
				from map in Mapper.Instance.GetTableMapFor<TEntity>()
				from sel in Extract<TModel>.From(map.Table)
				from whr in GetPredicates(map.Columns, predicates)
				select (map, sel, whr)
			)
			.Map(
				x => GetQuery(x.map.Name, x.sel, x.whr),
				e => new Msg.ErrorGettingGeneralRetrieveQueryExceptionMsg(e)
			);

		/// <summary>
		/// Convert LINQ expression property selectors to column names
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="columns">Mapped entity columns</param>
		/// <param name="predicates">Predicates (matched using AND)</param>
		internal static Option<List<(IColumn column, SearchOperator op, object value)>> GetPredicates<TEntity>(
			IMappedColumnList columns,
			(Expression<Func<TEntity, object>> column, SearchOperator op, object value)[] predicates
		)
			where TEntity : IEntity
		{
			var list = new List<(IColumn, SearchOperator, object)>();
			foreach (var item in predicates)
			{
				// The property name is the column alias
				var alias = item.column.GetPropertyInfo()?.Name;
				if (alias is null)
				{
					continue;
				}

				// Retrieve column using alias
				var column = columns.SingleOrDefault(c => c.Alias == alias);
				if (column is null)
				{
					continue;
				}

				// If predicate is IN, make sure it is a list
				if (item.op == SearchOperator.In && !item.value.GetType().Implements<IList>())
				{
					throw new InvalidQueryPredicateException("'IN' search operator requires value to be a list.");
				}

				// Add to list of predicates using column name
				list.Add((column, item.op, item.value));
			}

			return list;
		}

		/// <summary>
		/// Turn list of predicates into WHERE clauses with associated parameters
		/// </summary>
		/// <param name="predicates">List of predicates</param>
		/// <param name="includeTableName">If true, column names will be namespaced with the table name (necessary in JOIN queries)</param>
		protected (List<string> where, IQueryParameters param) GetWhereAndParameters(
			List<(IColumn column, SearchOperator op, object value)> predicates,
			bool includeTableName
		)
		{
			// Create empty lists
			var where = new List<string>();
			var param = new QueryParameters();
			var index = 0;

			// Loop through predicates and add each one
			foreach (var (column, op, value) in predicates)
			{
				// Escape column with or without table
				var escapedColumn = includeTableName switch
				{
					true =>
						Escape(column.Name, column.Table),

					false =>
						Escape(column)
				};

				// IN is a special case, handle ordinary cases first
				if (op != SearchOperator.In)
				{
					var paramName = $"P{index++}";
					param.Add(paramName, value);

					where.Add($"{escapedColumn} {GetOperator(op)} @{paramName}");

					continue;
				}

				// IN requires value to be a list of items
				if (value is IList list)
				{
					// Add a parameter for each value
					var inParam = new List<string>();
					foreach (var inValue in list)
					{
						var inParamName = $"P{index++}";

						param.Add(inParamName, inValue);
						inParam.Add(inParamName);
					}

					// If there are IN parameters, add to the query
					if (inParam.Count > 0)
					{
						where.Add($"{escapedColumn} {GetOperator(op)} {JoinList(inParam, true)}");
					}
				}
			}

			// Return
			return (where, param);
		}

		#endregion

		#region CRUD Queries

		/// <inheritdoc/>
		public Option<string> GetCreateQuery<TEntity>()
			where TEntity : IEntity =>
			Mapper.Instance.GetTableMapFor<TEntity>().Map(
				x => GetCreateQuery(x.Name, x.Columns),
				e => new Msg.ErrorGettingCrudCreateQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetCreateQuery{TEntity}"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of mapped columns</param>
		protected abstract string GetCreateQuery(
			string table,
			IMappedColumnList columns
		);

		/// <inheritdoc/>
		public Option<string> GetRetrieveQuery<TEntity, TModel>(long id)
			where TEntity : IEntity =>
			(
				from map in Mapper.Instance.GetTableMapFor<TEntity>()
				from col in Extract<TModel>.From(map.Table)
				select (map, col)
			)
			.Map(
				x => GetRetrieveQuery(x.map.Name, x.col, x.map.IdColumn, id),
				e => new Msg.ErrorGettingCrudRetrieveQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetRetrieveQuery{TEntity, TModel}(long)"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		protected abstract string GetRetrieveQuery(
			string table,
			ColumnList columns,
			IColumn idColumn,
			long id
		);

		/// <inheritdoc/>
		public Option<string> GetUpdateQuery<TEntity, TModel>(long id)
			where TEntity : IEntity =>
			(
				from map in Mapper.Instance.GetTableMapFor<TEntity>()
				from col in Extract<TModel>.From(map.Table)
				select (map, col)
			)
			.Map(
				x => typeof(TEntity).Implements<IEntityWithVersion>() switch
				{
					false =>
						GetUpdateQuery(x.map.Name, x.col, x.map.IdColumn, id),

					true =>
						GetUpdateQuery(x.map.Name, x.col, x.map.IdColumn, id, x.map.VersionColumn),

				},
				e => new Msg.ErrorGettingCrudUpdateQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetUpdateQuery(string, ColumnList, IColumn, long, IColumn)"/>
		protected abstract string GetUpdateQuery(
			string table,
			ColumnList columns,
			IColumn idColumn,
			long id
		);

		/// <inheritdoc cref="GetUpdateQuery{TEntity, TModel}"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to update</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		/// <param name="versionColumn">Version column for predicate</param>
		protected abstract string GetUpdateQuery(
			string table,
			ColumnList columns,
			IColumn idColumn,
			long id,
			IColumn? versionColumn
		);

		/// <inheritdoc/>
		public Option<string> GetDeleteQuery<TEntity>(long id)
			where TEntity : IEntity =>
			Mapper.Instance.GetTableMapFor<TEntity>().Map(
				x => typeof(TEntity).Implements<IEntityWithVersion>() switch
				{
					false =>
						GetDeleteQuery(x.Name, x.IdColumn, id),

					true =>
						GetDeleteQuery(x.Name, x.IdColumn, id, x.VersionColumn),

				},
				e => new Msg.ErrorGettingCrudDeleteQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetDeleteQuery(string, IColumn, long, IMappedColumn?)"/>
		protected abstract string GetDeleteQuery(
			string table,
			IColumn idColumn,
			long id
		);

		/// <inheritdoc cref="GetDeleteQuery{TEntity}"/>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		/// <param name="versionColumn">Version column for predicate</param>
		protected abstract string GetDeleteQuery(
			string table,
			IColumn idColumn,
			long id,
			IColumn? versionColumn
		);

		#endregion

		#region Testing

		internal (string query, IQueryParameters param) GetQueryTest(
			string table,
			ColumnList columns,
			List<(IColumn column, SearchOperator op, object value)> predicates
		) =>
			GetQuery(table, columns, predicates);

		internal string GetCreateQueryTest(string table, IMappedColumnList columns) =>
			GetCreateQuery(table, columns);

		internal string GetRetrieveQueryTest(string table, ColumnList columns, IColumn idColumn, long id) =>
			GetRetrieveQuery(table, columns, idColumn, id);

		internal string GetUpdateQueryTest(string table, ColumnList columns, IColumn idColumn, long id) =>
			GetUpdateQuery(table, columns, idColumn, id);

		internal string GetUpdateQueryTest(string table, ColumnList columns, IColumn idColumn, long id, IColumn? versionColumn) =>
			GetUpdateQuery(table, columns, idColumn, id, versionColumn);

		internal string GetDeleteQueryTest(string table, IColumn idColumn, long id) =>
			GetDeleteQuery(table, idColumn, id);

		internal string GetDeleteQueryTest(string table, IColumn idColumn, long id, IColumn? versionColumn) =>
			GetDeleteQuery(table, idColumn, id, versionColumn);

		#endregion

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Error getting General Retrieve query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingGeneralRetrieveQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error getting CRUD Create query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingCrudCreateQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error getting CRUD Retrieve query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingCrudRetrieveQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error getting CRUD Update query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingCrudUpdateQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error getting CRUD Delete query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingCrudDeleteQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
