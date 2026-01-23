// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
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
	/// <param name="alias">Column alias.</param>
	public static Result<IColumn> GetColumnFromAlias<TTable>(TTable table, string alias)
		where TTable : ITable =>
		table.GetProperties()
			.Where(x => x.Name == alias)
			.SingleOrNone()
			.ToResult(
				() => R.Fail(
						"Column with alias '{Alias}' not found in table '{Table}'.",
						new { alias, Table = table.GetType().Name }
					)
					.Ctx(nameof(QueryF), nameof(GetColumnFromAlias))
			)
			.Map(
				x => (name: x.GetValue(table)?.ToString()!, prop: x)
			)
			.ContinueIf(
				x => !string.IsNullOrEmpty(x.name),
				x => R.Fail(
						"Column with alias '{Alias}' has null or empty name in table '{Table}'.",
						new { alias, Table = table.GetType().Name }
					)
					.Ctx(nameof(QueryF), nameof(GetColumnFromAlias))
			)
			.Map(
				x => (IColumn)new Column(table.GetName(), x.name, x.prop)
			);

	/// <inheritdoc cref="GetColumnFromAlias{TTable}(TTable, string)"/>
	public static Result<IColumn> GetColumnFromAlias<TTable>(string columnAlias)
		where TTable : ITable, new() =>
		GetColumnFromAlias(new TTable(), columnAlias);
}
