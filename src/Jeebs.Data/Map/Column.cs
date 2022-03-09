// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections;
using System.Collections.Generic;

namespace Jeebs.Data.Map;

/// <inheritdoc cref="IColumn"/>
/// <param name="Table">Table Name</param>
/// <param name="Name">Column Name</param>
/// <param name="Alias">Column Alias</param>
public record class Column(ITableName TblName, string ColName, string ColAlias) : IColumn
{
	/// <summary>
	/// Create column using table object
	/// </summary>
	/// <param name="table">Table</param>
	/// <param name="colName">Column Name</param>
	/// <param name="colAlias">Column Alias</param>
	public Column(ITable table, string colName, string colAlias) : this(table.GetName(), colName, colAlias) { }

	/// <summary>
	/// Column Alias Comparer
	/// </summary>
	public class AliasComparer : IEqualityComparer<IColumn>, IEqualityComparer
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

		/// <inheritdoc cref="Equals(IColumn?, IColumn?)"/>
		public new bool Equals(object? x, object? y)
		{
			if (x == y)
			{
				return true;
			}

			if (x == null || y == null)
			{
				return false;
			}

			if (x is IColumn a && y is IColumn b)
			{
				return Equals(a, b);
			}

			return false;
		}

		/// <inheritdoc cref="GetHashCode(IColumn)"/>
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}

			if (obj is IColumn x)
			{
				return GetHashCode(x);
			}

			return obj.GetHashCode();
		}
	}

	/// <summary>
	/// Return column name
	/// </summary>
	public sealed override string ToString() =>
		ColName;
}
