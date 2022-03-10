// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jeebs.Data.Attributes;
using Jeebs.Messages;
using Maybe;
using Maybe.Functions;

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
	public IMappedColumnList Columns { get; init; }

	/// <inheritdoc/>
	public IMappedColumn IdColumn { get; init; }

	/// <inheritdoc/>
	public IMappedColumn? VersionColumn { get; internal set; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="table">Table</param>
	/// <param name="columns">Mapped Columns</param>
	/// <param name="idColumn">Id Column</param>
	public TableMap(ITable table, IMappedColumnList columns, IMappedColumn idColumn) =>
		(Table, Columns, IdColumn) = (table, columns, idColumn);

	/// <inheritdoc/>
	public IEnumerable<string> GetColumnNames() =>
		Columns.Select(mc => mc.ColName);

	/// <inheritdoc/>
	public IEnumerable<string> GetColumnAliases(bool includeIdAlias) =>
		Columns.Select(mc => mc.ColAlias).Where(a => includeIdAlias || a != IdColumn.ColAlias);

	/// <inheritdoc/>
	public Maybe<(List<string> names, List<string> aliases)> GetWriteableColumnNamesAndAliases() =>
		MaybeF.Some(
			() => from c in Columns
				  where c.PropertyInfo.GetCustomAttribute<IdAttribute>() is null
				  && c.PropertyInfo.GetCustomAttribute<ComputedAttribute>() is null
				  && c.PropertyInfo.GetCustomAttribute<ReadonlyAttribute>() is null
				  select c,
			MaybeF.DefaultHandler
		)
		.Bind(
			x => x.Any() switch
			{
				true =>
					MaybeF.Some(x),

				false =>
					MaybeF.None<IEnumerable<IMappedColumn>, M.NoWriteableColumnsFoundMsg>()
			}
		)
		.Map(
			x => (x.Select(w => w.ColName).ToList(), x.Select(w => w.ColAlias).ToList()),
			MaybeF.DefaultHandler
		);

	/// <inheritdoc cref="Name"/>
	public override string ToString() =>
		Name.GetFullName(s => s);

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>No writeable columns found (i.e. they are all marked as Id / Ignore / Computed)</summary>
		public sealed record class NoWriteableColumnsFoundMsg : Msg;
	}
}
