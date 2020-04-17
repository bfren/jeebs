using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Builds a Query in a fluent manner
	/// </summary>
	public sealed class QueryBuilder : IQueryBuilder
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		private readonly IUnitOfWork unitOfWork;

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="unitOfWork">IUnitOfWork</param>
		internal QueryBuilder(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

		/// <summary>
		/// Query Stage 1: Set the model for this query
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		public IQueryWithModel<T> WithModel<T>()
		{
			return new QueryBuilder<T>.QueryWithModel(unitOfWork);
		}
	}
}
