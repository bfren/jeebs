// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Reflection;
using Jeebs.Data.Map;
using Jeebs.Reflection;

namespace Jeebs.Data.Functions;

public static partial class DataF
{
	/// <summary>
	/// Build a column object from a column alias.
	/// </summary>
	/// <typeparam name="TTable">Table type.</typeparam>
	/// <param name="table">Table object.</param>
	/// <param name="alias">Column alias.</param>
	public static Result<IColumn> GetColumnFromAlias<TTable>(TTable table, string alias)
		where TTable : ITable =>
		table.GetProperties()
			.Where(x => x.Name == alias)
			.SingleOrNone()
			.ToResult(
				() => ColumnAliasNotFound(alias, table.GetType().Name)
			)
			.Map(
				x => (name: x.GetValue(table)?.ToString()!, prop: x)
			)
			.If(
				x => string.IsNullOrEmpty(x.name),
				x => ColumnAliasEmptyName(alias, table.GetType().Name)
			)
			.Map(
				x => (IColumn)new Column(table.GetName(), x.name, x.prop)
			);

	/// <inheritdoc cref="GetColumnFromAlias{TTable}(TTable, string)"/>
	public static Result<IColumn> GetColumnFromAlias<TTable>(string columnAlias)
		where TTable : ITable, new() =>
		GetColumnFromAlias(new TTable(), columnAlias);

	[Failure("Column with alias '{Alias}' not found in table '{Table}'.")]
	private static partial Failure ColumnAliasNotFound(string alias, string table);

	[Failure("Column with alias '{Alias}' has null or empty name in table '{Table}'.")]
	private static partial Failure ColumnAliasEmptyName(string alias, string table);
}
