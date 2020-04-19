using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Taxonomy
		/// </summary>
		/// <typeparam name="T">Term type</typeparam>
		/// <param name="modifyOptions">[Optional] Action to modify the options for this query</param>
		public async Task<IResult<List<T>>> QueryTaxonomyAsync<T>(Action<QueryTaxonomy.Options>? modifyOptions = null)
		{
			// Get terms
			var query = StartNewQuery()
				.WithModel<T>()
				.WithOptions(modifyOptions)
				.WithParts(new QueryTaxonomy.Builder<T>(db))
				.GetQuery();

			// Execute query
			var results = await query.ExecuteQueryAsync();
			if (results.Err is IErrorList)
			{
				return Result.Failure<List<T>>(results.Err);
			}

			// If nothing matches, return Not Found
			if (!results.Val.Any())
			{
				return Result.NotFound<List<T>>();
			}

			// Return success
			return Result.Success(results.Val.ToList());
		}

		/// <summary>
		/// Simple query - get terms from a particular taxonomy, with optional sorting
		/// </summary>
		/// <typeparam name="T">Term type</typeparam>
		/// <param name="taxonomy">Taxonomy to get</param>
		/// <param name="all">If true, will return all terms (even if the count is 0)</param>
		/// <param name="sort">[Optional] Sort columns</param>
		public async Task<IResult<List<T>>> QueryTaxonomyAsync<T>(Taxonomy taxonomy, bool all = true, params (string column, SortOrder order)[] sort)
		{
			return await QueryTaxonomyAsync<T>(opt =>
			{
				opt.Taxonomy = taxonomy;
				opt.Sort = sort;

				if (all)
				{
					opt.CountAtLeast = null;
				}
			});
		}
	}
}
