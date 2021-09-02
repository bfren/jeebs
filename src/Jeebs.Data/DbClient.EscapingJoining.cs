// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;

namespace Jeebs.Data;

public abstract partial class DbClient : IDbClient
{
	/// <inheritdoc/>
	public abstract string Escape(IColumn column, bool withAlias = false);

	/// <inheritdoc/>
	public abstract string EscapeWithTable(IColumn column, bool withAlias = false);

	/// <inheritdoc/>
	public abstract string Escape(ITable table);

	/// <inheritdoc/>
	public abstract string Escape(string columnOrTable);

	/// <inheritdoc/>
	public abstract string Escape(string column, string table);

	/// <inheritdoc/>
	public abstract string GetOperator(Compare cmp);

	/// <inheritdoc/>
	public abstract string GetParamRef(string paramName);

	/// <inheritdoc/>
	public abstract string JoinList(List<string> objects, bool wrap);
}
