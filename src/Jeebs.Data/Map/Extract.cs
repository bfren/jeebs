// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using Jeebs.Data.Functions;

namespace Jeebs.Data.Map;

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
/// <typeparam name="TModel">Model type.</typeparam>
public static partial class Extract<TModel>
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
					  from column in DataF.GetColumnsFromTable<TModel>(table)
					  select column,
				ErrorGettingColumns
			)
			.IfNot(
				x => x.Any(),
				_ => NoColumnsExtracted()
			)
			.Map(
				x => x.Distinct(new Column.AliasComparer()),
				ErrorGettingDistinctColumns
			)
			.Map(
				x => (IColumnList)new ColumnList(x)
			);
	}

	[Failure("Error getting columns from table.")]
	private static partial Failure ErrorGettingColumns(Exception e);

	[Failure("No columns were extracted from the tables.")]
	private static partial Failure NoColumnsExtracted();

	[Failure("Error getting distinct columns from the list.")]
	private static partial Failure ErrorGettingDistinctColumns(Exception e);
}
