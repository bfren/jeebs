// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Collections;

namespace Jeebs.Data.Map;

/// <inheritdoc cref="IColumnList"/>
public sealed record class ColumnList : ImmutableList<IColumn>, IColumnList
{
	/// <summary>
	/// Create empty list
	/// </summary>
	public ColumnList() { }

	/// <summary>
	/// Construct object from IEnumerable
	/// </summary>
	/// <param name="collection">IEnumerable</param>
	public ColumnList(IEnumerable<IColumn> collection) : base(collection) { }
}
