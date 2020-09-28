using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryBuilder"/>
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
		internal QueryBuilder(IUnitOfWork unitOfWork)
			=> this.unitOfWork = unitOfWork;

		/// <inheritdoc/>
		public IQueryWithModel<T> WithModel<T>()
			=> new QueryBuilder<T>.QueryWithModel(unitOfWork);
	}

	/// <summary>
	/// Query Builder
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	public sealed partial class QueryBuilder<TModel> { }
}
