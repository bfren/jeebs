// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// Fluently build a database query
	/// </summary>
	public interface IQueryBuilder
	{
		/// <summary>
		/// Set the main table to select data from
		/// </summary>
		/// <param name="table">ITable</param>
		IQueryBuilderWithFrom From(ITable table);

		/// <inheritdoc cref="From(ITable)"/>
		/// <typeparam name="TTable">Table type</typeparam>
		IQueryBuilderWithFrom From<TTable>()
			where TTable : ITable, new();
	}
}
