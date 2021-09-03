// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;

namespace Jeebs.Data.Mapping;

/// <inheritdoc cref="IMappedColumn"/>
/// <param name="Table">Table name</param>
/// <param name="Name">Column Name</param>
/// <param name="Property">Entity property PropertyInfo</param>
public sealed record class MappedColumn(string Table, string Name, PropertyInfo Property) :
	Column(Table, Name, Property.Name), IMappedColumn
{
	/// <summary>
	/// Create mapped column using table object
	/// </summary>
	/// <param name="Table">Table</param>
	/// <param name="Name">Column Name</param>
	/// <param name="Property">Entity property PropertyInfo</param>
	public MappedColumn(ITable Table, string Name, PropertyInfo Property) : this(Table.GetName(), Name, Property) { }

	/// <summary>
	/// Create from a mapped column interface
	/// </summary>
	/// <param name="mappedColumn">IMappedColumn</param>
	public MappedColumn(IMappedColumn mappedColumn) :
		this(
			mappedColumn.Table,
			mappedColumn.Name,
			mappedColumn.Property
		)
	{ }

	/// <summary>
	/// Return table name
	/// </summary>
	public override string ToString() =>
		Name;
}
