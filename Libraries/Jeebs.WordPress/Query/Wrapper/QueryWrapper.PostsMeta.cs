// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Data.Querying;
using static JeebsF.OptionF;

namespace Jeebs.WordPress
{
	public sealed partial class QueryWrapper
	{
		/// <summary>
		/// Query Posts Meta
		/// </summary>
		/// <typeparam name="TModel">Term type</typeparam>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		public Task<Option<List<TModel>>> QueryPostsMetaAsync<TModel>(Action<QueryPostsMeta.Options>? modify = null)
			where TModel : IEntity
		{
			return Return(modify)
				.Map(
					GetPostsMetaQuery<TModel>
				)
				.BindAsync(
					x => x.ExecuteQueryAsync()
				);
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
