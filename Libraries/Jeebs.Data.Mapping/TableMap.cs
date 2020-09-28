using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Table Map
	/// </summary>
	public sealed class TableMap
	{
		/// <summary>
		/// Escaped Table Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Mapped Columns
		/// </summary>
		public List<MappedColumn> Columns { get; set; }

		/// <summary>
		/// Id Column
		/// </summary>
		public MappedColumn IdColumn { get; set; }

		/// <summary>
		/// [Optional] Version Column
		/// </summary>
		public MappedColumn? VersionColumn { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="name">Table Name - must be escaped</param>
		/// <param name="columns">Mapped Columns</param>
		/// <param name="idColumn">Id Column</param>
		public TableMap(string name, List<MappedColumn> columns, MappedColumn idColumn)
			=> (Name, Columns, IdColumn) = (name, columns, idColumn);

		/// <summary>
		/// Get all column names (they will be escaped)
		/// </summary>
		public IEnumerable<string> GetColumnNames()
			=> Columns.Select(mc => mc.Column);

		/// <summary>
		/// Get all column aliases
		/// </summary>
		/// <param name="includeIdAlias">If true, the ID column alias will be included</param>
		public IEnumerable<string> GetAliases(bool includeIdAlias)
			=> Columns.Select(mc => mc.Property.Name).Where(a => includeIdAlias || a != IdColumn.Property.Name);

		/// <summary>
		/// Get all column names and aliases for writeable columns
		/// (i.e. not marked with <see cref="IdAttribute"/>, <see cref="ComputedAttribute"/> or <see cref="ReadonlyAttribute"/>
		/// </summary>
		public (List<string> columns, List<string> aliases) GetWriteableColumnsAndAliases()
		{
			// Query
			var writeable = from c in Columns
							where c.Property.GetCustomAttribute<IdAttribute>() == null
							&& c.Property.GetCustomAttribute<ComputedAttribute>() == null
							&& c.Property.GetCustomAttribute<ReadonlyAttribute>() == null
							select c;

			if (!writeable.Any())
			{
				throw new Jx.Data.MappingException($"Table {Name} does not have any writeable columns.");
			}

			// Return
			return (
				writeable.Select(w => w.Column).ToList(),
				writeable.Select(w => w.Property.Name).ToList()
			);
		}

		/// <summary>
		/// Return Escaped Table Name
		/// </summary>
		public override string ToString()
			=> Name;
	}
}
