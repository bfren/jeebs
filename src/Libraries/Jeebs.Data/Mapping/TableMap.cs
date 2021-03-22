// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static F.OptionF;

namespace Jeebs.Data
{
	/// <inheritdoc cref="ITableMap"/>
	public sealed record TableMap : ITableMap
	{
		/// <inheritdoc/>
		public ITable Table { get; init; }

		/// <inheritdoc/>
		public string Name =>
			Table.TableName;

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
			Columns.Select(mc => mc.Name);

		/// <inheritdoc/>
		public IEnumerable<string> GetColumnAliases(bool includeIdAlias) =>
			Columns.Select(mc => mc.Alias).Where(a => includeIdAlias || a != IdColumn.Alias);

		/// <inheritdoc/>
		public Option<(List<string> names, List<string> aliases)> GetWriteableColumnNamesAndAliases() =>
			Return(
				() => from c in Columns
					  where c.Property.GetCustomAttribute<IdAttribute>() == null
					  && c.Property.GetCustomAttribute<ComputedAttribute>() == null
					  && c.Property.GetCustomAttribute<ReadonlyAttribute>() == null
					  select c,
				DefaultHandler
			)
			.Bind(
				x => x.Any() switch
				{
					true =>
						Return(x),

					false =>
						None<IEnumerable<IMappedColumn>, Msg.NoWriteableColumnsFoundMsg>()
				}
			)
			.Map(
				x => (x.Select(w => w.Name).ToList(), x.Select(w => w.Alias).ToList()),
				DefaultHandler
			);

		/// <summary>
		/// Return Table Name
		/// </summary>
		public override string ToString() =>
			Name;

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>No writeable columns found (i.e. they are all marked as Id / Ignore / Computed)</summary>
			public sealed record NoWriteableColumnsFoundMsg : IMsg { }
		}
	}
}
