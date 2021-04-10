// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.Data.Querying
{
	/// <summary>
	/// Saves query options (stage 2) and enables stage 3: build query parts
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <typeparam name="TOptions">QueryOptions</typeparam>
	public interface IQueryWithOptions<TModel, TOptions>
		where TOptions : IQueryOptions
	{
		/// <summary>
		/// Query Stage 3: Use existing query parts
		/// </summary>
		/// <param name="parts">IQueryParts</param>
		IQueryWithParts<TModel> WithParts(IQueryParts parts);

		/// <summary>
		/// Query Stage 3: Build the query parts
		/// </summary>
		/// <param name="builder">IQueryPartsBuilder</param>
		IQueryWithParts<TModel> WithParts(IQueryPartsBuilder<TModel, TOptions> builder);
	}
}
