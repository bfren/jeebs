// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;

namespace Jeebs.Data.Map;

/// <inheritdoc cref="IMappedColumn"/>
/// <param name="Table">Table name</param>
/// <param name="Name">Column Name</param>
/// <param name="PropertyInfo">Entity property PropertyInfo</param>
public sealed record class MappedColumn(ITableName Table, string Name, PropertyInfo PropertyInfo) :
	Column(Table, Name, PropertyInfo.Name), IMappedColumn
{
	/// <summary>
	/// Create mapped column using table object
	/// </summary>
	/// <param name="table">Table</param>
	/// <param name="name">Column Name</param>
	/// <param name="propertyInfo">Entity property PropertyInfo</param>
	public MappedColumn(ITable table, string name, PropertyInfo propertyInfo) : this(table.GetName(), name, propertyInfo) { }

	/// <summary>
	/// Create from a mapped column interface
	/// </summary>
	/// <param name="mappedColumn">IMappedColumn</param>
	public MappedColumn(IMappedColumn mappedColumn) :
		this(
			mappedColumn.TblName,
			mappedColumn.ColName,
			mappedColumn.PropertyInfo
		)
	{ }
}
