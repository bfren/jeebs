using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <inheritdoc cref="IQueryWrapper"/>
	public class QueryWrapper : IQueryWrapper
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		protected readonly IUnitOfWork unitOfWork;

		/// <summary>
		/// Setup object - start a new IUnitOfWork for this query
		/// </summary>
		/// <param name="db">IDb</param>
		public QueryWrapper(IDb db) => unitOfWork = db.UnitOfWork;

		/// <inheritdoc/>
		public IQueryBuilder StartNewQuery() => new QueryBuilder(unitOfWork);

		/// <summary>
		/// Dispose <see cref="IUnitOfWork"/>
		/// </summary>
		public void Dispose() => unitOfWork.Dispose();
	}
}
