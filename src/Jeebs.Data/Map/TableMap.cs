// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.Data.Attributes;

namespace Jeebs.Data.Map;

/// <inheritdoc cref="ITableMap"/>
public sealed record class TableMap : ITableMap
{
	/// <inheritdoc/>
	public ITable Table { get; init; }

	/// <inheritdoc/>
	public ITableName Name =>
		Table.GetName();

	/// <inheritdoc/>
	public IColumnList Columns { get; init; }

	/// <inheritdoc/>
	public IColumn IdColumn { get; init; }

	/// <inheritdoc/>
	public IColumn? VersionColumn { get; internal set; }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="table">Table.</param>
	/// <param name="columns">Mapped Columns.</param>
	/// <param name="idColumn">Id Column.</param>
	public TableMap(ITable table, IColumnList columns, IColumn idColumn) =>
		(Table, Columns, IdColumn) = (table, columns, idColumn);

	/// <inheritdoc/>
	public IEnumerable<string> GetColumnNames() =>
		Columns.Select(mc => mc.ColName);

	/// <inheritdoc/>
	public IEnumerable<string> GetColumnAliases(bool includeIdAlias) =>
		Columns.Select(mc => mc.ColAlias).Where(a => includeIdAlias || a != IdColumn.ColAlias);

	/// <inheritdoc/>
	public Result<(List<string> names, List<string> aliases)> GetWriteableColumnNamesAndAliases() =>
		R.Try(
			() => from c in Columns
				  where c.PropertyInfo.GetCustomAttribute<IdAttribute>() is null
				  && c.PropertyInfo.GetCustomAttribute<ComputedAttribute>() is null
				  && c.PropertyInfo.GetCustomAttribute<ReadonlyAttribute>() is null
				  select c
		)
		.ContinueIf(x => x.Any(),
			_ => R.Fail("No writeable columns found.")
				.Ctx(nameof(TableMap), nameof(GetWriteableColumnNamesAndAliases))
		)
		.Map(
			x => (x.Select(w => w.ColName).ToList(), x.Select(w => w.ColAlias).ToList())
		);

	/// <inheritdoc cref="Name"/>
	public override string ToString() =>
		Name.GetFullName(s => s);
}
