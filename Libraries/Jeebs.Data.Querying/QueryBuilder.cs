// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryBuilder"/>
	public sealed class QueryBuilder : IQueryBuilder
	{
		/// <summary>
		/// IUnitOfWork
		/// </summary>
		internal IUnitOfWork UnitOfWork { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="unitOfWork">IUnitOfWork</param>
		internal QueryBuilder(IUnitOfWork unitOfWork) =>
			UnitOfWork = unitOfWork;

		/// <inheritdoc/>
		public IQueryWithModel<T> WithModel<T>() =>
			new QueryBuilder<T>.QueryWithModel(UnitOfWork);
	}

	/// <summary>
	/// Query Builder
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	public sealed partial class QueryBuilder<TModel> { }
}
