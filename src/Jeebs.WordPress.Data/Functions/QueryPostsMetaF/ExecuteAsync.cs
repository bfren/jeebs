// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
		/// <typeparam name="TModel">Return Model type</typeparam>
		/// <param name="db">IWpDbQuery</param>
		/// <param name="opt">Function to return query options</param>
		public static Task<Option<IEnumerable<TModel>>> ExecuteAsync<TModel>(
			IWpDb db,
			Query.GetPostsMetaOptions opt
		)
			where TModel : IWithId<WpPostMetaId>
		{
			return
				Return(
					() => opt(new Query.PostsMetaOptions(db.Schema)),
					e => new Msg.ErrorGettingQueryPostsMetaOptionsMsg(e)
				)
				.Bind(
					x => x.ToParts<TModel>()
				)
				.BindAsync(
					x => db.Query.QueryAsync<TModel>(x)
				);
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get posts meta query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryPostsMetaOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
