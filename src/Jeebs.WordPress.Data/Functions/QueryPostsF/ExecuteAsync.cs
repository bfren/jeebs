// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryPostsF
	{
		/// <summary>
		/// Get query parts using the specific options
		/// </summary>
		/// <typeparam name="TModel">Return value type</typeparam>
		/// <param name="db">IWpDb</param>
		/// <param name="opt">Function to return query options</param>
		internal static Option<IQueryParts> GetQueryParts<TModel>(
			IWpDb db,
			Query.GetPostsOptions opt
		)
			where TModel : IWithId<WpPostId>
		{
			return
				Return(
					() => opt(new Query.PostsOptions(db)),
					e => new Msg.ErrorGettingQueryPostsOptionsMsg(e)
				)
				.Bind(
					x => x.ToParts<TModel>()
				);
		}

		/// <inheritdoc cref="ExecuteAsync{TModel}(IWpDb, long, Query.GetPostsOptions)"/>
		internal static Task<Option<IEnumerable<TModel>>> ExecuteAsync<TModel>(
			IWpDb db,
			Query.GetPostsOptions opt
		)
			where TModel : IWithId<WpPostId>
		{
			return
				GetQueryParts<TModel>(
					db, opt
				)
				.BindAsync(
					x => db.Query.QueryAsync<TModel>(x)
				)
				.BindAsync(
					x => x.Count() switch
					{
						> 0 =>
							Process<IEnumerable<TModel>, TModel>(db, x),

						_ =>
							Return(x).AsTask
					}
				);
		}

		/// <summary>
		/// Run a query and return multiple items with paging
		/// </summary>
		/// <typeparam name="TModel">Return value type</typeparam>
		/// <param name="db">IWpDb</param>
		/// <param name="page">Page number</param>
		/// <param name="opt">Function to return query options</param>
		internal static Task<Option<IPagedList<TModel>>> ExecuteAsync<TModel>(
			IWpDb db,
			long page,
			Query.GetPostsOptions opt
		)
			where TModel : IWithId<WpPostId>
		{
			return
				GetQueryParts<TModel>(
					db, opt
				)
				.BindAsync(
					x => db.Query.QueryAsync<TModel>(page, x)
				)
				.BindAsync(
					x => x switch
					{
						PagedList<TModel> when x.Count > 0 =>
							Process<IPagedList<TModel>, TModel>(db, x),

						PagedList<TModel> =>
							Return(x).AsTask,

						_ =>
							None<IPagedList<TModel>, Msg.UnrecognisedPagedListTypeMsg>().AsTask
					}
				);
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get posts query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryPostsOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Unrecognised <see cref="IPagedList{T}"/> implementation</summary>
			public sealed record UnrecognisedPagedListTypeMsg : IMsg { }
		}
	}
}
