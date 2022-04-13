// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Jeebs.Data.Map;

/// <inheritdoc cref="IColumn"/>
public sealed record class Column : IColumn
{
	/// <summary>
	/// Table name
	/// </summary>
	public IDbName TblName { get; init; }

	/// <summary>
	/// Column name
	/// </summary>
	public string ColName { get; init; }

	/// <summary>
	/// Column alias
	/// </summary>
	public string ColAlias =>
		PropertyInfo.Name;

	/// <summary>
	/// Table property PropertyInfo
	/// </summary>
	public PropertyInfo PropertyInfo { get; init; }

	/// <summary>
	/// Create column
	/// </summary>
	/// <param name="tblName">Table name</param>
	/// <param name="colName">Column Name</param>
	/// <param name="propertyInfo">Entity property PropertyInfo</param>
	public Column(IDbName tblName, string colName, PropertyInfo propertyInfo) =>
		(TblName, ColName, PropertyInfo) = (tblName, colName, propertyInfo);

	/// <summary>
	/// Create column using table object
	/// </summary>
	/// <param name="table">Table</param>
	/// <param name="colName">Column Name</param>
	/// <param name="propertyInfo">Entity property PropertyInfo</param>
	public Column(ITable table, string colName, PropertyInfo propertyInfo) : this(table.GetName(), colName, propertyInfo) { }

	/// <summary>
	/// Return column name
	/// </summary>
	public override string ToString() =>
		ColName;

	/// <summary>
	/// Column Alias Comparer
	/// </summary>
	public sealed class AliasComparer : IEqualityComparer<IColumn>, IEqualityComparer
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
}
