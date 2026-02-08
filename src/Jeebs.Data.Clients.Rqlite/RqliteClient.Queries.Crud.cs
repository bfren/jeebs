// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Clients.Rqlite;

public partial class RqliteClient
{
	/// <inheritdoc/>
	protected override string GetCreateQuery(ITableName table, IColumnList columns) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	protected override string GetRetrieveQuery(ITableName table, IColumnList columns, IColumn idColumn, object id) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	protected override string GetUpdateQuery(ITableName table, IColumnList columns, IColumn idColumn, object id) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	protected override string GetUpdateQuery(ITableName table, IColumnList columns, IColumn idColumn, object id, IColumn? versionColumn) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	protected override string GetDeleteQuery(ITableName table, IColumn idColumn, object id) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	protected override string GetDeleteQuery(ITableName table, IColumn idColumn, object id, IColumn? versionColumn) => throw new System.NotImplementedException();
}
