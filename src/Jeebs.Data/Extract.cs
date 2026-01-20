// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Jeebs.Data.Map;
using Jeebs.Data.Query.Functions;

namespace Jeebs.Data;

/// <inheritdoc cref="IExtract"/>
public sealed class Extract : IExtract
{
	/// <inheritdoc/>
	public Result<IColumnList> From<TModel>(params ITable[] tables) =>
		Extract<TModel>.From(tables);
}

/// <summary>
/// Extract columns from a table that match <typeparamref name="TModel"/>.
/// </summary>
/// <typeparam name="TModel">Model type</typeparam>
public static class Extract<TModel>
{
	/// <summary>
	/// Extract columns from specified tables.
	/// </summary>
	/// <param name="tables">List of tables.</param>
#pragma warning disable CA1000 // Do not declare static members on generic types
	public static Result<IColumnList> From(params ITable[] tables)
#pragma warning restore CA1000 // Do not declare static members on generic types
	{
		// If no tables, return empty extracted list
		if (tables.Length == 0)
		{
			return new ColumnList();
		}

		// Extract distinct columns
		return
			R.Try(
				() => from table in tables
					  from column in QueryF.GetColumnsFromTable<TModel>(table)
					  select column,
				e => R.Fail(nameof(Extract), nameof(From),
					e, "Error getting columns from table."
				)
			)
			.ContinueIf(
				x => x.Any(),
				_ => R.Fail(nameof(Extract), nameof(From),
					"No columns were extracted from the tables."
				)
			)
			.Map(
				x => x.Distinct(new Column.AliasComparer()),
				e => R.Fail(nameof(Extract), nameof(From),
					e, "Error getting distinct columns from the list."
				)
			)
			.Map(
				x => (IColumnList)new ColumnList(x)
			);
	}
}
