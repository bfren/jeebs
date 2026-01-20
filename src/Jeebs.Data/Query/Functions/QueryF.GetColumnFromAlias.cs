// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Data.Map;
using Jeebs.Reflection;

namespace Jeebs.Data.Query.Functions;

public static partial class QueryF
{
	/// <summary>
	/// Build a column object from a column alias.
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="table">Table object.</param>
	/// <param name="columnAlias">Column alias.</param>
	public static Result<IColumn> GetColumnFromAlias<TTable>(TTable table, string columnAlias)
		where TTable : ITable =>
		table.GetProperties()
			.SingleOrNone()
			.ToResult(nameof(QueryF), nameof(GetColumnFromAlias))
			.ContinueIf(
				x => x.Name == columnAlias,
				x => R.Fail(nameof(QueryF), nameof(GetColumnFromAlias),
					"Column with alias '{Alias}' not found in table '{Table}'.", columnAlias, table.GetName()
				)
			)
			.Map(
				x => (name: x.GetValue(table)?.ToString()!, prop: x)
			)
			.ContinueIf(
				x => !string.IsNullOrEmpty(x.name),
				x => R.Fail(nameof(QueryF), nameof(GetColumnFromAlias),
					"Column with alias '{Alias}' has null or empty name in table '{Table}'.", columnAlias, table.GetName()
				)
			)
			.Map(
				x => (IColumn)new Column(table.GetName(), x.name, x.prop)
			);

	/// <inheritdoc cref="GetColumnFromAlias{TTable}(TTable, string)"/>
	public static Result<IColumn> GetColumnFromAlias<TTable>(string columnAlias)
		where TTable : ITable, new() =>
		GetColumnFromAlias(
			new TTable(), columnAlias
		);
}
