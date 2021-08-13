// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryBuilder"/>
	public sealed record class QueryBuilder : IQueryBuilder
	{
		/// <summary>
		/// Internal creation only
		/// </summary>
		internal QueryBuilder() { }

		/// <inheritdoc/>
		public IQueryBuilderWithFrom From(ITable table) =>
			new QueryBuilderWithFrom(table);

		/// <inheritdoc/>
		public IQueryBuilderWithFrom From<TTable>()
			where TTable : ITable, new() =>
			From(new TTable());
	}
}
