using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Saves query options (stage 2) and enables stage 3: build query parts
	/// </summary>
	/// <typeparam name="TOptions">QueryOptions</typeparam>
	public interface IQueryWithOptions<TOptions>
		where TOptions : QueryOptions
	{
		/// <summary>
		/// Query Stage 3: Use existing query parts
		/// </summary>
		/// <typeparam name="TModel">Model Type</typeparam>
		/// <param name="parts">IQueryParts</param>
		public IQuery<TModel> WithParts<TModel>(IQueryParts<TModel> parts);

		/// <summary>
		/// Query Stage 3: Build the query parts
		/// </summary>
		/// <typeparam name="TModel">Model Type</typeparam>
		/// <param name="builder">IQueryPartsBuilder</param>
		public IQuery<TModel> WithBuilder<TModel>(IQueryPartsBuilder<TModel, TOptions> builder);
	}
}
