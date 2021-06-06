// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Querying;
using static F.OptionF;

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
			where TModel : IEntity =>
			Return(modify)
				.Bind(
					GetPostsMetaQuery<TModel>
				)
				.BindAsync(
					x => x.ExecuteQueryAsync()
				);

		/// <summary>
		/// Get query object
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		/// <param name="modify">[Optional] Action to modify the options for this query</param>
		private Option<IQuery<TModel>> GetPostsMetaQuery<TModel>(Action<QueryPostsMeta.Options>? modify = null) =>
			Return(
				() => StartNewQuery()
						.WithModel<TModel>()
						.WithOptions(modify)
						.WithParts(new QueryPostsMeta.Builder<TModel>(db))
						.GetQuery(),
				e => new Msg.GetPostsMetaQueryExceptionMsg(e)
			);

		public static partial class Msg
		{
			/// <summary>Unable to get posts meta query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record GetPostsMetaQueryExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
