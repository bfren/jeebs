﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Mapping;

/// <inheritdoc cref="ITable"/>
public abstract record class Table : ITable
{
	private readonly ITableName name;

	/// <summary>
	/// Create with <see cref="ITableName"/>
	/// </summary>
	/// <param name="name">ITableName</param>
	protected Table(ITableName name) =>
		this.name = name;

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
		name;

	/// <summary>
	/// See <see cref="ITableName.GetFullName(System.Func{string, string})"/>
	/// </summary>
	public sealed override string ToString() =>
		name.GetFullName(s => s);
}
