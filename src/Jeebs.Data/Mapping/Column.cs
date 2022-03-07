// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs.Data.Mapping;

/// <inheritdoc cref="IColumn"/>
/// <param name="Table">Table Name</param>
/// <param name="Name">Column Name</param>
/// <param name="Alias">Column Alias</param>
public partial record class Column(ITableName TblName, string ColName, string ColAlias) : IColumn
{
	/// <summary>
	/// Create column using table object
	/// </summary>
	/// <param name="Table">Table</param>
	/// <param name="Name">Column Name</param>
	/// <param name="Alias">Column Alias</param>
	public Column(ITable Table, string Name, string Alias) : this(Table.GetName(), Name, Alias) { }

	/// <summary>
	/// Column Alias Comparer
	/// </summary>
	public class AliasComparer : IEqualityComparer<IColumn>
	{
		/// <summary>
		/// Returns true if the two aliases are identical
		/// </summary>
		/// <param name="x">IColumn 1</param>
		/// <param name="y">IColumn 2</param>
		public bool Equals(IColumn? x, IColumn? y) =>
			x?.ColAlias == y?.ColAlias;

		/// <summary>
		/// Return object's hash code
		/// </summary>
		/// <param name="obj">IColumn</param>
		public int GetHashCode(IColumn obj) =>
			obj.ColAlias.GetHashCode();
	}

	/// <summary>
	/// Return column name
	/// </summary>
	public sealed override string ToString() =>
		ColName;
}
