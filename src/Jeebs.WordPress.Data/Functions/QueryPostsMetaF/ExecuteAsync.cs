// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsMetaF
	{
		/// <summary>
		/// Execute Posts Meta query
		/// </summary>
		/// <typeparam name="TPostMeta">Post Meta Entity type</typeparam>
		/// <typeparam name="TModel">Return Model type</typeparam>
		/// <param name="query">IWpDbQuery</param>
		/// <param name="opt">Function to return query options</param>
		public static Task<Option<IEnumerable<TModel>>> ExecuteAsync<TPostMeta, TModel>(
			IWpDbQuery query,
			Query.GetPostsMetaOptions<TPostMeta> opt
		)
			where TPostMeta : WpPostMetaEntity
			where TModel : IWithId =>
			Return(
				() => opt(new Query.PostsMetaOptions<TPostMeta>(query.Db)),
				e => new Msg.ErrorGettingQueryPostsMetaOptionsMsg(e)
			)
			.Bind(
				x => x.GetParts<TModel>()
			)
			.BindAsync(
				x => query.QueryAsync<TModel>(x)
			);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get posts meta query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryPostsMetaOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
