// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;
using Jeebs.Linq;

namespace Jeebs.Data
{
	public abstract partial class DbClient : IDbClient
	{
		/// <inheritdoc/>
		public Option<string> GetCreateQuery<TEntity>()
			where TEntity : IWithId =>
			Mapper.GetTableMapFor<TEntity>().Map(
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
		public Option<string> GetRetrieveQuery<TEntity, TModel>(ulong id)
			where TEntity : IWithId =>
			(
				from map in Mapper.GetTableMapFor<TEntity>()
				from columns in Extract<TModel>.From(map.Table)
				select (map, columns)
			)
			.Map(
				x => GetRetrieveQuery(x.map.Name, x.columns, x.map.IdColumn, id),
				e => new Msg.ErrorGettingCrudRetrieveQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetRetrieveQuery{TEntity, TModel}(ulong)"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		protected abstract string GetRetrieveQuery(
			string table,
			IColumnList columns,
			IColumn idColumn,
			ulong id
		);

		/// <inheritdoc/>
		public Option<string> GetUpdateQuery<TEntity, TModel>(ulong id)
			where TEntity : IWithId =>
			(
				from map in Mapper.GetTableMapFor<TEntity>()
				from columns in Extract<TModel>.From(map.Table)
				select (map, columns)
			)
			.Map(
				x => typeof(TEntity).Implements<IWithVersion>() switch
				{
					false =>
						GetUpdateQuery(x.map.Name, x.columns, x.map.IdColumn, id),

					true =>
						GetUpdateQuery(x.map.Name, x.columns, x.map.IdColumn, id, x.map.VersionColumn),

				},
				e => new Msg.ErrorGettingCrudUpdateQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetUpdateQuery(string, IColumnList, IColumn, ulong, IColumn)"/>
		protected abstract string GetUpdateQuery(
			string table,
			IColumnList columns,
			IColumn idColumn,
			ulong id
		);

		/// <inheritdoc cref="GetUpdateQuery{TEntity, TModel}"/>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to update</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		/// <param name="versionColumn">Version column for predicate</param>
		protected abstract string GetUpdateQuery(
			string table,
			IColumnList columns,
			IColumn idColumn,
			ulong id,
			IColumn? versionColumn
		);

		/// <inheritdoc/>
		public Option<string> GetDeleteQuery<TEntity>(ulong id)
			where TEntity : IWithId =>
			Mapper.GetTableMapFor<TEntity>().Map(
				x => typeof(TEntity).Implements<IWithVersion>() switch
				{
					false =>
						GetDeleteQuery(x.Name, x.IdColumn, id),

					true =>
						GetDeleteQuery(x.Name, x.IdColumn, id, x.VersionColumn),

				},
				e => new Msg.ErrorGettingCrudDeleteQueryExceptionMsg(e)
			);

		/// <inheritdoc cref="GetDeleteQuery(string, IColumn, ulong, IColumn?)"/>
		protected abstract string GetDeleteQuery(
			string table,
			IColumn idColumn,
			ulong id
		);

		/// <inheritdoc cref="GetDeleteQuery{TEntity}"/>
		/// <param name="table">Table name</param>
		/// <param name="idColumn">ID column for predicate</param>
		/// <param name="id">Entity ID</param>
		/// <param name="versionColumn">Version column for predicate</param>
		protected abstract string GetDeleteQuery(
			string table,
			IColumn idColumn,
			ulong id,
			IColumn? versionColumn
		);

		#region Testing

		internal string GetCreateQueryTest(string table, IMappedColumnList columns) =>
			GetCreateQuery(table, columns);

		internal string GetRetrieveQueryTest(string table, IColumnList columns, IColumn idColumn, ulong id) =>
			GetRetrieveQuery(table, columns, idColumn, id);

		internal string GetUpdateQueryTest(string table, IColumnList columns, IColumn idColumn, ulong id) =>
			GetUpdateQuery(table, columns, idColumn, id);

		internal string GetUpdateQueryTest(string table, IColumnList columns, IColumn idColumn, ulong id, IColumn? versionColumn) =>
			GetUpdateQuery(table, columns, idColumn, id, versionColumn);

		internal string GetDeleteQueryTest(string table, IColumn idColumn, ulong id) =>
			GetDeleteQuery(table, idColumn, id);

		internal string GetDeleteQueryTest(string table, IColumn idColumn, ulong id, IColumn? versionColumn) =>
			GetDeleteQuery(table, idColumn, id, versionColumn);

		#endregion
	}
}
