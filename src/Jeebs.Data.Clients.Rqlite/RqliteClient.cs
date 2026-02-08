// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;

namespace Jeebs.Data.Clients.Rqlite;

/// <inheritdoc cref="IDbClient"/>
public partial class RqliteClient : DbClient
{
	/// <inheritdoc/>
	public override string Escape(ITableName table) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	public override string Escape(ITableName table, string column) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	public override string Escape(IColumn column, bool withAlias) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	public override string Escape(string obj) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	public override string EscapeWithTable(IColumn column, bool withAlias) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	public override string GetOperator(Compare cmp) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	public override string GetParamRef(string paramName) => throw new System.NotImplementedException();

	/// <inheritdoc/>
	public override string JoinList(List<string> objects, bool wrap) => throw new System.NotImplementedException();
}
