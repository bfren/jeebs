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
		public Option<string> GetRetrieveQuery<TEntity, TModel>()
			where TEntity : IEntity =>
			(
				from map in Mapper.Instance.GetTableMapFor<TEntity>()
				from col in Extract<TModel>.From(map.Table)
				select (map, col)
			)
			.Map(
				x => GetRetrieveQuery(x.map.Name, x.col, x.map.IdColumn),
				e => new Msg.ErrorGettingRetrieveQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetRetrieveQuery{TEntity, TModel}"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="idColumn">ID column for predicate</param>
		protected abstract string GetRetrieveQuery(string table, ColumnList columns, IMappedColumn idColumn);

		/// <inheritdoc/>
		public Option<string> GetUpdateQuery<TEntity, TModel>()
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
						GetUpdateQuery(x.map.Name, x.col, x.map.IdColumn),

					true =>
						GetUpdateQuery(x.map.Name, x.col, x.map.IdColumn, x.map.VersionColumn),

				},
				e => new Msg.ErrorGettingUpdateQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetUpdateQuery(string, ColumnList, IMappedColumn, IMappedColumn)"/>
		protected abstract string GetUpdateQuery(string table, ColumnList columns, IMappedColumn idColumn);

		/// <inheritdoc cref="GetUpdateQuery{TEntity, TModel}"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to update</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="versionColumn">Version column for predicate</param>
		protected abstract string GetUpdateQuery(string table, ColumnList columns, IMappedColumn idColumn, IMappedColumn? versionColumn);

		/// <inheritdoc/>
		public Option<string> GetDeleteQuery<TEntity>()
			where TEntity : IEntity =>
			Mapper.Instance.GetTableMapFor<TEntity>().Map(
				x => GetDeleteQuery(x.Name, x.IdColumn),
				e => new Msg.ErrorGettingDeleteQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetDeleteQuery{TEntity}"/>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column for predicate</param>
		protected abstract string GetDeleteQuery(string table, IMappedColumn idColumn);

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
