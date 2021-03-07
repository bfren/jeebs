// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Table Map
	/// </summary>
	public sealed class TableMap
	{
		/// <summary>
		/// Table Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Mapped Columns
		/// </summary>
		public IMappedColumnList Columns { get; set; }

		/// <summary>
		/// Id Column
		/// </summary>
		public IMappedColumn IdColumn { get; set; }

		/// <summary>
		/// [Optional] Version Column
		/// </summary>
		public IMappedColumn? VersionColumn { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Table Name - must be escaped</param>
		/// <param name="columns">Mapped Columns</param>
		/// <param name="idColumn">Id Column</param>
		public TableMap(string name, IMappedColumnList columns, IMappedColumn idColumn) =>
			(Name, Columns, IdColumn) = (name, columns, idColumn);

		/// <summary>
		/// Get all column names
		/// </summary>
		public IEnumerable<string> GetColumnNames() =>
			Columns.Select(mc => mc.Name);

		/// <summary>
		/// Get all column aliases
		/// </summary>
		/// <param name="includeIdAlias">If true, the ID column alias will be included</param>
		public IEnumerable<string> GetColumnAliases(bool includeIdAlias) =>
			Columns.Select(mc => mc.Alias).Where(a => includeIdAlias || a != IdColumn.Alias);

		/// <summary>
		/// Get all column names and aliases for writeable columns
		/// (i.e. not marked with <see cref="IdAttribute"/>, <see cref="ComputedAttribute"/> or <see cref="ReadonlyAttribute"/>
		/// </summary>
		public (List<string> names, List<string> aliases) GetWriteableColumnNamesAndAliases()
		{
			// Search for writeatble columns
			var writeable = from c in Columns
							where c.Property.GetCustomAttribute<IdAttribute>() == null
							&& c.Property.GetCustomAttribute<ComputedAttribute>() == null
							&& c.Property.GetCustomAttribute<ReadonlyAttribute>() == null
							select c;

			if (!writeable.Any())
			{
				throw new Jx.Data.Mapping.NoWriteableColumnsException(Name);
			}

			// Return
			return (
				writeable.Select(w => w.Name).ToList(),
				writeable.Select(w => w.Alias).ToList()
			);
		}

		/// <summary>
		/// Return Escaped Table Name
		/// </summary>
		public override string ToString() =>
			Name;
	}
}
