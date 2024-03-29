// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using StrongId;

namespace Jeebs.Data;

public abstract partial class DbClient : IDbClient
{
	/// <inheritdoc/>
	public Maybe<string> GetCreateQuery<TEntity>()
		where TEntity : IWithId =>
		Entities.GetTableMapFor<TEntity>().Map(
			x => GetCreateQuery(x.Name, x.Columns),
			e => new M.ErrorGettingCrudCreateQueryExceptionMsg(e)
		);

	/// <inheritdoc cref="GetCreateQuery{TEntity}"/>
	/// <param name="table">Table name</param>
	/// <param name="columns">List of mapped columns</param>
	protected abstract string GetCreateQuery(
		IDbName table,
		IColumnList columns
	);

	/// <inheritdoc/>
	public Maybe<string> GetRetrieveQuery<TEntity, TModel>(object id)
		where TEntity : IWithId =>
		(
			from map in Entities.GetTableMapFor<TEntity>()
			from columns in Extract<TModel>.From(map.Table)
			select (map, columns)
		)
		.Map(
			x => GetRetrieveQuery(x.map.Name, x.columns, x.map.IdColumn, id),
			e => new M.ErrorGettingCrudRetrieveQueryExceptionMsg(e)
		);

	/// <inheritdoc cref="GetRetrieveQuery{TEntity, TModel}(object)"/>
	/// <param name="table">Table name</param>
	/// <param name="columns">List of columns to select</param>
	/// <param name="idColumn">ID column for predicate</param>
	/// <param name="id">Entity ID</param>
	protected abstract string GetRetrieveQuery(
		IDbName table,
		IColumnList columns,
		IColumn idColumn,
		object id
	);

	/// <inheritdoc/>
	public Maybe<string> GetUpdateQuery<TEntity, TModel>(object id)
		where TEntity : IWithId =>
		(
			from map in Entities.GetTableMapFor<TEntity>()
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
			e => new M.ErrorGettingCrudUpdateQueryExceptionMsg(e)
		);

	/// <inheritdoc cref="GetUpdateQuery(IDbName, IColumnList, IColumn, object, IColumn)"/>
	protected abstract string GetUpdateQuery(
		IDbName table,
		IColumnList columns,
		IColumn idColumn,
		object id
	);

	/// <inheritdoc cref="GetUpdateQuery{TEntity, TModel}"/>
	/// <param name="table">Table name</param>
	/// <param name="columns">List of columns to update</param>
	/// <param name="idColumn">ID column for predicate</param>
	/// <param name="id">Entity ID</param>
	/// <param name="versionColumn">Version column for predicate</param>
	protected abstract string GetUpdateQuery(
		IDbName table,
		IColumnList columns,
		IColumn idColumn,
		object id,
		IColumn? versionColumn
	);

	/// <inheritdoc/>
	public Maybe<string> GetDeleteQuery<TEntity>(object id)
		where TEntity : IWithId =>
		Entities.GetTableMapFor<TEntity>().Map(
			x => typeof(TEntity).Implements<IWithVersion>() switch
			{
				false =>
					GetDeleteQuery(x.Name, x.IdColumn, id),

				true =>
					GetDeleteQuery(x.Name, x.IdColumn, id, x.VersionColumn),

			},
			e => new M.ErrorGettingCrudDeleteQueryExceptionMsg(e)
		);

	/// <inheritdoc cref="GetDeleteQuery(IDbName, IColumn, object, IColumn?)"/>
	protected abstract string GetDeleteQuery(
		IDbName table,
		IColumn idColumn,
		object id
	);

	/// <inheritdoc cref="GetDeleteQuery{TEntity}"/>
	/// <param name="table">Table name</param>
	/// <param name="idColumn">ID column for predicate</param>
	/// <param name="id">Entity ID</param>
	/// <param name="versionColumn">Version column for predicate</param>
	protected abstract string GetDeleteQuery(
		IDbName table,
		IColumn idColumn,
		object id,
		IColumn? versionColumn
	);

	#region Testing

	internal string GetCreateQueryTest(IDbName table, IColumnList columns) =>
		GetCreateQuery(table, columns);

	internal string GetRetrieveQueryTest(IDbName table, IColumnList columns, IColumn idColumn, long id) =>
		GetRetrieveQuery(table, columns, idColumn, id);

	internal string GetUpdateQueryTest(IDbName table, IColumnList columns, IColumn idColumn, long id) =>
		GetUpdateQuery(table, columns, idColumn, id);

	internal string GetUpdateQueryTest(IDbName table, IColumnList columns, IColumn idColumn, long id, IColumn? versionColumn) =>
		GetUpdateQuery(table, columns, idColumn, id, versionColumn);

	internal string GetDeleteQueryTest(IDbName table, IColumn idColumn, long id) =>
		GetDeleteQuery(table, idColumn, id);

	internal string GetDeleteQueryTest(IDbName table, IColumn idColumn, long id, IColumn? versionColumn) =>
		GetDeleteQuery(table, idColumn, id, versionColumn);

	#endregion Testing
}
