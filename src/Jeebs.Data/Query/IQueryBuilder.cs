// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Query;

/// <summary>
/// Fluently build a database query.
/// </summary>
public interface IQueryBuilder
{
	/// <summary>
	/// Set the main table to select data from.
	/// </summary>
	/// <param name="table">ITable.</param>
	IQueryBuilderWithFrom From(ITable table);

	/// <inheritdoc cref="From(ITable)"/>
	/// <typeparam name="TTable">Table type.</typeparam>
	IQueryBuilderWithFrom From<TTable>()
		where TTable : ITable, new();
}
