using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Taxonomy
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="modifyOptions">[Optional] Action to modify the options for this query</param>
		public async Task<IResult<IEnumerable<T>>> QueryTaxonomyAsync<T>(Action<QueryTaxonomy.Options>? modifyOptions = null)
		{
			return await StartNewQuery()
				.WithModel<T>()
				.WithOptions(modifyOptions)
				.WithParts(new QueryTaxonomy.Builder<T>(db))
				.GetQuery()
				.ExecuteQueryAsync();
		}
	}
}
