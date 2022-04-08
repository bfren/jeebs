// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map;

/// <inheritdoc cref="ITable"/>
public abstract record class Table : ITable
{
	private ITableName TableName { get; init; }

	/// <summary>
	/// Create with <see cref="ITableName"/>
	/// </summary>
	/// <param name="name">ITableName</param>
	protected Table(ITableName name) =>
		TableName = name;

	/// <summary>
	/// Create with table name
	/// </summary>
	/// <param name="name">Table Name</param>
	protected Table(string name) : this(new TableName(name)) { }

	/// <summary>
	/// Create with table schema and name
	/// </summary>
	/// <param name="schema">Table Schema</param>
	/// <param name="name">Table Name</param>
	protected Table(string schema, string name) : this(new TableName(schema, name)) { }

	/// <inheritdoc/>
	public ITableName GetName() =>
		TableName;

	/// <summary>
	/// See <see cref="ITableName.GetFullName(System.Func{string, string})"/>
	/// </summary>
	public sealed override string ToString() =>
		TableName.GetFullName(s => s);
}

/// <summary>
/// Represents a null / unknown table
/// </summary>
public sealed record class NullTable() : Table(string.Empty);
