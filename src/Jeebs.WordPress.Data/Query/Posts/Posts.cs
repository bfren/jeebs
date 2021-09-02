// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using F.WordPressF.DataF;
using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Querying;

namespace Jeebs.WordPress.Data;

public static partial class Query
{
	/// <inheritdoc cref="IQueryPosts"/>
	public sealed class Posts : IQueryPosts
	{
		/// <inheritdoc/>
		public Task<Option<IEnumerable<T>>> ExecuteAsync<T>(
			IWpDb db,
			IUnitOfWork w,
			GetPostsOptions opt,
			params IContentFilter[] filters
		)
			where T : IWithId<WpPostId>
		{
			return QueryPostsF.ExecuteAsync<T>(db, w, opt, filters);
		}

		/// <inheritdoc/>
		public Task<Option<IPagedList<T>>> ExecuteAsync<T>(
			IWpDb db,
			IUnitOfWork w,
			ulong page,
			GetPostsOptions opt,
			params IContentFilter[] filters
		)
			where T : IWithId<WpPostId>
		{
			return QueryPostsF.ExecuteAsync<T>(db, w, page, opt, filters);
		}
	}
}
