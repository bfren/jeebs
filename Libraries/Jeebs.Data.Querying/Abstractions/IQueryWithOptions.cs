using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// Saves query options (stage 2) and enables stage 3: build query parts
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	/// <typeparam name="TOptions">QueryOptions</typeparam>
	public interface IQueryWithOptions<TModel, TOptions>
		where TOptions : QueryOptions
	{
		/// <summary>
		/// Query Stage 3: Use existing query parts
		/// </summary>
		/// <param name="parts">IQueryParts</param>
		public IQueryWithParts<TModel> WithParts(IQueryParts parts);

		/// <summary>
		/// Query Stage 3: Build the query parts
		/// </summary>
		/// <param name="builder">IQueryPartsBuilder</param>
		public IQueryWithParts<TModel> WithParts(IQueryPartsBuilder<TModel, TOptions> builder);
	}
}
