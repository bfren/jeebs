using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryWrapper"/>
	public class QueryWrapper : IQueryWrapper
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		internal IUnitOfWork UnitOfWork { get; }

		/// <summary>
		/// Setup object - start a new IUnitOfWork for this query
		/// </summary>
		/// <param name="db">IDb</param>
		public QueryWrapper(IDb db)
			=> UnitOfWork = db.UnitOfWork;

		/// <inheritdoc/>
		public IQueryBuilder StartNewQuery()
			=> new QueryBuilder(UnitOfWork);

		/// <summary>
		/// Dispose <see cref="IUnitOfWork"/>
		/// </summary>
		public void Dispose()
			=> UnitOfWork.Dispose();
	}
}
