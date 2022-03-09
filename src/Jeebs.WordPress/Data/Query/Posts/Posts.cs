// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.WordPress.Data.ContentFilters;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Functions;
using Jeebs.WordPress.Data.Querying;
using Maybe;

namespace Jeebs.WordPress.Data;

public static partial class Query
{
	/// <inheritdoc cref="IQueryPosts"/>
	public sealed class Posts : IQueryPosts
	{
		/// <inheritdoc/>
		public Task<Maybe<IEnumerable<T>>> ExecuteAsync<T>(
			IWpDb db,
			IUnitOfWork w,
			GetPostsOptions opt,
			params IContentFilter[] filters
		)
			where T : Id.IWithId<WpPostId> =>
			QueryPostsF.ExecuteAsync<T>(db, w, opt, filters);

		/// <inheritdoc/>
		public Task<Maybe<IPagedList<T>>> ExecuteAsync<T>(
			IWpDb db,
			IUnitOfWork w,
			ulong page,
			GetPostsOptions opt,
			params IContentFilter[] filters
		)
			where T : Id.IWithId<WpPostId> =>
			QueryPostsF.ExecuteAsync<T>(db, w, page, opt, filters);
	}
}
