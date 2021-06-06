// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Querying
{
	public sealed partial class QueryBuilder<TModel>
	{
		/// <inheritdoc cref="IQueryWithParts{T}"/>
		public sealed class QueryWithParts : IQueryWithParts<TModel>
		{
			/// <summary>
			/// IUnitOfWork
			/// </summary>
			internal IUnitOfWork UnitOfWork { get; }

			/// <summary>
			/// IQueryParts
			/// </summary>
			internal IQueryParts Parts { get; }

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="unitOfWork">IUnitOfWork</param>
			/// <param name="parts">TOptions</param>
			internal QueryWithParts(IUnitOfWork unitOfWork, IQueryParts parts) =>
				(UnitOfWork, Parts) = (unitOfWork, parts);

			/// <inheritdoc/>
			public IQuery<TModel> GetQuery() =>
				new Query<TModel>(UnitOfWork, Parts);
		}
	}
}
