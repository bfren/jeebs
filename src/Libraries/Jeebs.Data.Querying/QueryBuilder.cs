// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryBuilder"/>
	public sealed record QueryBuilder : IQueryBuilder
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
