﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;

namespace Jeebs.Data;

public abstract partial class DbClient : IDbClient
{
	/// <inheritdoc/>
	public string Escape(ITable table) =>
		Escape(table.GetName());

	/// <inheritdoc/>
	public abstract string Escape(ITableName table);

	/// <inheritdoc/>
	public abstract string Escape(ITableName table, string column);

	/// <inheritdoc/>
	public abstract string Escape(IColumn column, bool withAlias = false);

	/// <inheritdoc/>
	public abstract string EscapeWithTable(IColumn column, bool withAlias = false);

	/// <inheritdoc/>
	public abstract string Escape(string obj);

	/// <inheritdoc/>
	public abstract string GetOperator(Compare cmp);

	/// <inheritdoc/>
	public abstract string GetParamRef(string paramName);

	/// <inheritdoc/>
	public abstract string JoinList(List<string> objects, bool wrap);
}
