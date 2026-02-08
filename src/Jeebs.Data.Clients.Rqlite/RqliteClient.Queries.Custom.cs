// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query;

namespace Jeebs.Data.Clients.Rqlite;

public partial class RqliteClient
{
	/// <inheritdoc/>
	protected override Result<(string query, IQueryParametersDictionary param)> GetQuery(ITableName table, IColumnList columns, IImmutableList<(IColumn column, Compare cmp, object value)> predicates) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	public override Result<(string query, IQueryParametersDictionary param)> GetQuery(IQueryParts parts) => throw new System.NotImplementedException();
}
