// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Data;
using Jeebs.Linq;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IDbClient"/>
	public abstract class DbClient : IDbClient
	{
		/// <inheritdoc/>
		public abstract IDbConnection Connect(string connectionString);

		#region General Queries

		#endregion

		#region CRUD Queries

		/// <inheritdoc/>
		public Option<string> GetCreateQuery<TEntity>()
			where TEntity : IEntity =>
			Mapper.Instance.GetTableMapFor<TEntity>().Map(
				x => GetCreateQuery(x.Name, x.Columns),
				e => new Msg.ErrorGettingCreateQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetCreateQuery{TEntity}"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of mapped columns</param>
		protected abstract string GetCreateQuery(string table, IMappedColumnList columns);

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
				e => new Msg.ErrorGettingRetrieveQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetRetrieveQuery{TEntity, TModel}"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		protected abstract string GetRetrieveQuery(string table, ColumnList columns, IMappedColumn idColumn, long id);

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
				e => new Msg.ErrorGettingUpdateQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetUpdateQuery(string, ColumnList, IMappedColumn, long, IMappedColumn)"/>
		protected abstract string GetUpdateQuery(string table, ColumnList columns, IMappedColumn idColumn, long id);

		/// <inheritdoc cref="GetUpdateQuery{TEntity, TModel}"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to update</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		/// <param name="versionColumn">Version column for predicate</param>
		protected abstract string GetUpdateQuery(string table, ColumnList columns, IMappedColumn idColumn, long id, IMappedColumn? versionColumn);

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
				e => new Msg.ErrorGettingDeleteQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetDeleteQuery(string, IMappedColumn, long, IMappedColumn?)"/>
		protected abstract string GetDeleteQuery(string table, IMappedColumn idColumn, long id);

		/// <inheritdoc cref="GetDeleteQuery{TEntity}"/>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		/// <param name="versionColumn">Version column for predicate</param>
		protected abstract string GetDeleteQuery(string table, IMappedColumn idColumn, long id, IMappedColumn? versionColumn);

		#endregion

		#region Testing

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
			/// <summary>Error getting Create query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingCreateQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error getting Retrieve query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingRetrieveQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error getting Update query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingUpdateQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Error getting Delete query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingDeleteQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
