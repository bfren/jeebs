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
		/// Table Name
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
		/// <param name="name">Table Name</param>
		/// <param name="columns">Mapped Columns</param>
		/// <param name="idColumn">Id Column</param>
		public TableMap(string name, List<MappedColumn> columns, MappedColumn idColumn)
		{
			Name = name;
			Columns = columns;
			IdColumn = idColumn;
		}

		public IEnumerable<string> GetColumnNames() => Columns.Select(mc => mc.Column);

		public IEnumerable<string> GetAliases(bool includeIdAlias) => Columns.Select(mc => mc.Property.Name).Where(a => includeIdAlias || a != IdColumn.Property.Name);

		public (List<string> columns, List<string> aliases) GetWriteableColumnsAndAliases()
		{
			var writeable = (
				from c in Columns
				where c.Property.GetCustomAttribute<IdAttribute>() == null
				&& c.Property.GetCustomAttribute<ComputedAttribute>() == null
				&& c.Property.GetCustomAttribute<ReadonlyAttribute>() == null
				select c
			).ToList();

			return (writeable.Select(w => w.Column).ToList(), writeable.Select(w => w.Property.Name).ToList());
		}
	}
}
