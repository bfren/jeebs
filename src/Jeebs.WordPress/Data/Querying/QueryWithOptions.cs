// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.Data.Querying
{
	public sealed partial class QueryBuilder<TModel>
	{
		/// <inheritdoc cref="IQueryWithOptions{TModel, TOptions}"/>
		public sealed class QueryWithOptions<TOptions> : IQueryWithOptions<TModel, TOptions>
			where TOptions : IQueryOptions
		{
			/// <summary>
			/// IUnitOfWork
			/// </summary>
			internal IUnitOfWork UnitOfWork { get; }

			/// <summary>
			/// TOptions
			/// </summary>
			internal TOptions Options { get; }

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="unitOfWork">IUnitOfWork</param>
			/// <param name="options">TOptions</param>
			internal QueryWithOptions(IUnitOfWork unitOfWork, TOptions options) =>
				(UnitOfWork, Options) = (unitOfWork, options);

			/// <inheritdoc/>
			public IQueryWithParts<TModel> WithParts(IQueryParts parts) =>
				new QueryWithParts(UnitOfWork, parts);

			/// <inheritdoc/>
			public IQueryWithParts<TModel> WithParts(IQueryPartsBuilder<TModel, TOptions> builder) =>
				WithParts(builder.Build(Options));
		}
	}
}
