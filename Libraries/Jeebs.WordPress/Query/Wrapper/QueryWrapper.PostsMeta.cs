using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Data.Querying;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Posts Meta
		/// </summary>
		/// <typeparam name="TModel">Term type</typeparam>
		/// <param name="r">Result</param>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		public async Task<IR<List<TModel>>> QueryPostsMetaAsync<TModel>(IOk r, Action<QueryPostsMeta.Options>? modify = null)
			where TModel : IEntity
		{
			// Get query
			var query = GetPostsMetaQuery<TModel>(modify);

			// Execute query
			return await query.ExecuteQueryAsync(r).ConfigureAwait(false);
		}

		/// <summary>
		/// Get query object
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		private IQuery<TModel> GetPostsMetaQuery<TModel>(Action<QueryPostsMeta.Options>? modify = null) =>
			StartNewQuery()
				.WithModel<TModel>()
				.WithOptions(modify)
				.WithParts(new QueryPostsMeta.Builder<TModel>(db))
				.GetQuery();
	}
}
