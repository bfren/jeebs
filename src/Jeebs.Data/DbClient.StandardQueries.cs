// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data;

public abstract partial class DbClient : IDbClient
{
	/// <inheritdoc/>
	public Result<string> GetCreateQuery<TEntity>()
		where TEntity : IWithId =>
		EntityMapper
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
	public Result<string> GetRetrieveQuery<TEntity, TModel>()
		where TEntity : IWithId =>
		(
			from map in EntityMapper.GetTableMapFor<TEntity>()
			from columns in Extract<TModel>.From(map.Table)
			select (map, columns)
		)
		.Map(
			x => GetRetrieveQuery(x.map.Name, x.columns, x.map.IdColumn)
		);

	/// <inheritdoc cref="GetRetrieveQuery{TEntity, TModel}(object)"/>
	/// <param name="table">Table name.</param>
	/// <param name="columns">List of columns to select.</param>
	/// <param name="idColumn">ID column for predicate.</param>
	protected abstract string GetRetrieveQuery(
		ITableName table,
		IColumnList columns,
		IColumn idColumn
	);

	/// <inheritdoc/>
	public Result<string> GetUpdateQuery<TEntity, TModel>()
		where TEntity : IWithId =>
		(
			from map in EntityMapper.GetTableMapFor<TEntity>()
			from columns in Extract<TModel>.From(map.Table)
			select (map, columns)
		)
		.Map(
			x => typeof(TEntity).Implements<IWithVersion>() switch
			{
				false =>
					GetUpdateQuery(x.map.Name, x.columns, x.map.IdColumn),

				true =>
					GetUpdateQuery(x.map.Name, x.columns, x.map.IdColumn, x.map.VersionColumn),
			}
		);

	/// <inheritdoc cref="GetUpdateQuery(ITableName, IColumnList, IColumn, IColumn)"/>
	protected abstract string GetUpdateQuery(
		ITableName table,
		IColumnList columns,
		IColumn idColumn
	);

	/// <inheritdoc cref="GetUpdateQuery{TEntity, TModel}"/>
	/// <param name="table">Table name.</param>
	/// <param name="columns">List of columns to update.</param>
	/// <param name="idColumn">ID column for predicate.</param>
	/// <param name="versionColumn">Version column for predicate.</param>
	protected abstract string GetUpdateQuery(
		ITableName table,
		IColumnList columns,
		IColumn idColumn,
		IColumn? versionColumn
	);

	/// <inheritdoc/>
	public Result<string> GetDeleteQuery<TEntity>()
		where TEntity : IWithId =>
		EntityMapper.GetTableMapFor<TEntity>().Map(
			x => typeof(TEntity).Implements<IWithVersion>() switch
			{
				false =>
					GetDeleteQuery(x.Name, x.IdColumn),

				true =>
					GetDeleteQuery(x.Name, x.IdColumn, x.VersionColumn),
			}
		);

	/// <inheritdoc cref="GetDeleteQuery(ITableName, IColumn, IColumn?)"/>
	protected abstract string GetDeleteQuery(
		ITableName table,
		IColumn idColumn
	);

	/// <inheritdoc cref="GetDeleteQuery{TEntity}"/>
	/// <param name="table">Table name.</param>
	/// <param name="idColumn">ID column for predicate.</param>
	/// <param name="versionColumn">Version column for predicate.</param>
	protected abstract string GetDeleteQuery(
		ITableName table,
		IColumn idColumn,
		IColumn? versionColumn
	);

	#region Testing

	internal string GetCreateQueryTest(ITableName table, IColumnList columns) =>
		GetCreateQuery(table, columns);

	internal string GetRetrieveQueryTest(ITableName table, IColumnList columns, IColumn idColumn) =>
		GetRetrieveQuery(table, columns, idColumn);

	internal string GetUpdateQueryTest(ITableName table, IColumnList columns, IColumn idColumn) =>
		GetUpdateQuery(table, columns, idColumn);

	internal string GetUpdateQueryTest(ITableName table, IColumnList columns, IColumn idColumn, IColumn? versionColumn) =>
		GetUpdateQuery(table, columns, idColumn, versionColumn);

	internal string GetDeleteQueryTest(ITableName table, IColumn idColumn) =>
		GetDeleteQuery(table, idColumn);

	internal string GetDeleteQueryTest(ITableName table, IColumn idColumn, IColumn? versionColumn) =>
		GetDeleteQuery(table, idColumn, versionColumn);

	#endregion Testing
}
