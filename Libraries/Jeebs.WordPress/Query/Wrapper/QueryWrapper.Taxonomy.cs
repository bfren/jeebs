// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data.Enums;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Taxonomy
		/// </summary>
		/// <typeparam name="TModel">Term type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		public async Task<IR<List<TModel>>> QueryTaxonomyAsync<TModel>(IOk r, Action<QueryTaxonomy.Options>? modify = null)
		{
			// Get terms
			var query = StartNewQuery()
				.WithModel<TModel>()
				.WithOptions(modify)
				.WithParts(new QueryTaxonomy.Builder<TModel>(db))
				.GetQuery();

			// Execute query
			return await query.ExecuteQueryAsync(r).ConfigureAwait(false);
		}

		/// <summary>
		/// Simple query - get terms from a particular taxonomy, with optional sorting
		/// </summary>
		/// <typeparam name="TModel">Term type</typeparam>
		/// <param name="r">Result: value is taxonomy to get</param>
		/// <param name="all">If true, will return all terms (even if the count is 0)</param>
		/// <param name="sort">[Optional] Sort columns</param>
		public Task<IR<List<TModel>>> QueryTaxonomyAsync<TModel>(IOkV<Taxonomy> r, bool all = true, params (string column, SortOrder order)[] sort) =>
			QueryTaxonomyAsync<TModel>(r, opt =>
			{
				opt.Taxonomy = r.Value;
				opt.Sort = sort;

				if (all)
				{
					opt.CountAtLeast = null;
				}
			});
	}
}
