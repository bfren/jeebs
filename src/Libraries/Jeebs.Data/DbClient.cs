// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Linq;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDbClient"/>
	public abstract class DbClient : IDbClient
	{
		/// <inheritdoc/>
		public abstract IDbConnection Connect(string connectionString);

		#region Custom Queries

		/// <summary>
		/// Return a query to retrieve a list of entities that match all the specified parameters
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="predicates">Predicates (matched using AND)</param>
		protected abstract (string query, Dictionary<string, object> param) GetRetrieveQuery(
			string table,
			ColumnList columns,
			List<(string column, SearchOperator op, object value)> predicates
		);

		/// <inheritdoc/>
		public Option<(string query, Dictionary<string, object> param)> GetRetrieveQuery<TEntity, TModel>(
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
				x => GetRetrieveQuery(x.map.Name, x.sel, x.whr),
				e => new Msg.ErrorGettingGeneralRetrieveQueryExceptionMsg(e)
			);

		/// <summary>
		/// Convert LINQ expression property selectors to column names
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="columns">Mapped entity columns</param>
		/// <param name="predicates">Predicates (matched using AND)</param>
		private static Option<List<(string column, SearchOperator op, object value)>> GetPredicates<TEntity>(
			IMappedColumnList columns,
			(Expression<Func<TEntity, object>> column, SearchOperator op, object value)[] predicates
		)
			where TEntity : IEntity
		{
			var list = new List<(string, SearchOperator, object)>();
			foreach (var item in predicates)
			{
				// The property name is the column alias
				var alias = item.column.GetPropertyInfo().Name;

				// Retrieve column using alias
				var column = columns.SingleOrDefault(c => c.Alias == alias);
				if (column == null)
				{
					continue;
				}

				// Add to list of predicates using column name
				list.Add((column.Name, item.op, item.value));
			}

			return list;
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
			IMappedColumn idColumn,
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

		/// <inheritdoc cref="GetUpdateQuery(string, ColumnList, IMappedColumn, long, IMappedColumn)"/>
		protected abstract string GetUpdateQuery(
			string table,
			ColumnList columns,
			IMappedColumn idColumn,
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
			IMappedColumn idColumn,
			long id,
			IMappedColumn? versionColumn
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

		/// <inheritdoc cref="GetDeleteQuery(string, IMappedColumn, long, IMappedColumn?)"/>
		protected abstract string GetDeleteQuery(
			string table,
			IMappedColumn idColumn,
			long id
		);

		/// <inheritdoc cref="GetDeleteQuery{TEntity}"/>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		/// <param name="versionColumn">Version column for predicate</param>
		protected abstract string GetDeleteQuery(
			string table,
			IMappedColumn idColumn,
			long id,
			IMappedColumn? versionColumn
		);

		#endregion

		#region Testing

		internal (string query, Dictionary<string, object> param) GetRetrieveQueryTest(
			string table,
			ColumnList columns,
			List<(string column, SearchOperator op, object value)> predicates
		) =>
			GetRetrieveQuery(table, columns, predicates);

		internal string GetCreateQueryTest(string table, IMappedColumnList columns) =>
			GetCreateQuery(table, columns);

		internal string GetRetrieveQueryTest(string table, ColumnList columns, IMappedColumn idColumn, long id) =>
			GetRetrieveQuery(table, columns, idColumn, id);

		internal string GetUpdateQueryTest(string table, ColumnList columns, IMappedColumn idColumn, long id) =>
			GetUpdateQuery(table, columns, idColumn, id);

		internal string GetUpdateQueryTest(string table, ColumnList columns, IMappedColumn idColumn, long id, IMappedColumn? versionColumn) =>
			GetUpdateQuery(table, columns, idColumn, id, versionColumn);

		internal string GetDeleteQueryTest(string table, IMappedColumn idColumn, long id) =>
			GetDeleteQuery(table, idColumn, id);

		internal string GetDeleteQueryTest(string table, IMappedColumn idColumn, long id, IMappedColumn? versionColumn) =>
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
