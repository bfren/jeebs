// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Common;

public abstract partial class DbClient : IDbClient
{
	/// <inheritdoc/>
	public Result<string> GetCreateQuery<TEntity>()
		where TEntity : IWithId =>
		Entities
			.GetTableMapFor<TEntity>()
			.Map(
				x => GetCreateQuery(x.Name, x.Columns)
			);

	/// <inheritdoc cref="GetCreateQuery{TEntity}"/>
	/// <param name="table">Table name.</param>
	/// <param name="columns">List of mapped columns.</param>
	protected abstract string GetCreateQuery(
		ITableName table,
		IColumnList columns
	);

	/// <inheritdoc/>
	public Result<string> GetRetrieveQuery<TEntity, TModel>(object id)
		where TEntity : IWithId =>
		(
			from map in Entities.GetTableMapFor<TEntity>()
			from columns in Extract<TModel>.From(map.Table)
			select (map, columns)
		)
		.Map(
			x => GetRetrieveQuery(x.map.Name, x.columns, x.map.IdColumn, id)
		);

	/// <inheritdoc cref="GetRetrieveQuery{TEntity, TModel}(object)"/>
	/// <param name="table">Table name.</param>
	/// <param name="columns">List of columns to select.</param>
	/// <param name="idColumn">ID column for predicate.</param>
	/// <param name="id">Entity ID.</param>
	protected abstract string GetRetrieveQuery(
		ITableName table,
		IColumnList columns,
		IColumn idColumn,
		object id
	);

	/// <inheritdoc/>
	public Result<string> GetUpdateQuery<TEntity, TModel>(object id)
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
			}
		);

	/// <inheritdoc cref="GetUpdateQuery(ITableName, IColumnList, IColumn, object, IColumn)"/>
	protected abstract string GetUpdateQuery(
		ITableName table,
		IColumnList columns,
		IColumn idColumn,
		object id
	);

	/// <inheritdoc cref="GetUpdateQuery{TEntity, TModel}"/>
	/// <param name="table">Table name.</param>
	/// <param name="columns">List of columns to update.</param>
	/// <param name="idColumn">ID column for predicate.</param>
	/// <param name="id">Entity ID.</param>
	/// <param name="versionColumn">Version column for predicate.</param>
	protected abstract string GetUpdateQuery(
		ITableName table,
		IColumnList columns,
		IColumn idColumn,
		object id,
		IColumn? versionColumn
	);

	/// <inheritdoc/>
	public Result<string> GetDeleteQuery<TEntity>(object id)
		where TEntity : IWithId =>
		Entities.GetTableMapFor<TEntity>().Map(
			x => typeof(TEntity).Implements<IWithVersion>() switch
			{
				false =>
					GetDeleteQuery(x.Name, x.IdColumn, id),

				true =>
					GetDeleteQuery(x.Name, x.IdColumn, id, x.VersionColumn),
			}
		);

	/// <inheritdoc cref="GetDeleteQuery(ITableName, IColumn, object, IColumn?)"/>
	protected abstract string GetDeleteQuery(
		ITableName table,
		IColumn idColumn,
		object id
	);

	/// <inheritdoc cref="GetDeleteQuery{TEntity}"/>
	/// <param name="table">Table name.</param>
	/// <param name="idColumn">ID column for predicate.</param>
	/// <param name="id">Entity ID.</param>
	/// <param name="versionColumn">Version column for predicate.</param>
	protected abstract string GetDeleteQuery(
		ITableName table,
		IColumn idColumn,
		object id,
		IColumn? versionColumn
	);

	#region Testing

	internal string GetCreateQueryTest(ITableName table, IColumnList columns) =>
		GetCreateQuery(table, columns);

	internal string GetRetrieveQueryTest(ITableName table, IColumnList columns, IColumn idColumn, long id) =>
		GetRetrieveQuery(table, columns, idColumn, id);

	internal string GetUpdateQueryTest(ITableName table, IColumnList columns, IColumn idColumn, long id) =>
		GetUpdateQuery(table, columns, idColumn, id);

	internal string GetUpdateQueryTest(ITableName table, IColumnList columns, IColumn idColumn, long id, IColumn? versionColumn) =>
		GetUpdateQuery(table, columns, idColumn, id, versionColumn);

	internal string GetDeleteQueryTest(ITableName table, IColumn idColumn, long id) =>
		GetDeleteQuery(table, idColumn, id);

	internal string GetDeleteQueryTest(ITableName table, IColumn idColumn, long id, IColumn? versionColumn) =>
		GetDeleteQuery(table, idColumn, id, versionColumn);

	#endregion Testing
}
