// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryWrapper"/>
	public class QueryWrapper : IQueryWrapper
	{
		/// <inheritdoc/>
		public IUnitOfWork UnitOfWork { get; }

		/// <summary>
		/// Setup object - start a new IUnitOfWork for this query
		/// </summary>
		/// <param name="db">IDb</param>
		public QueryWrapper(IDb db) =>
			UnitOfWork = db.UnitOfWork;

		/// <inheritdoc/>
		public IQueryBuilder StartNewQuery() =>
			new QueryBuilder(UnitOfWork);

		/// <summary>
		/// Dispose <see cref="IUnitOfWork"/>
		/// </summary>
		public void Dispose() =>
			UnitOfWork.Dispose();
	}
}
