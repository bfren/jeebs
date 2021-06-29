// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using F.WordPressF.DataF;
using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
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
				long page,
				GetPostsOptions opt,
				params IContentFilter[] filters
			)
				where T : IWithId<WpPostId>
			{
				return QueryPostsF.ExecuteAsync<T>(db, w, page, opt, filters);
			}
		}
	}
}
