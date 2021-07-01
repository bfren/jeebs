// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Query Posts - to enable testing of static functions
	/// </summary>
	public interface IQueryPosts
	{
		/// <inheritdoc cref="ExecuteAsync{T}(IWpDb, IUnitOfWork, ulong, Query.GetPostsOptions, IContentFilter[])"/>
		Task<Option<IEnumerable<T>>> ExecuteAsync<T>(
			IWpDb db,
			IUnitOfWork w,
			Query.GetPostsOptions opt,
			params IContentFilter[] filters
		)
			where T : IWithId<WpPostId>;

		/// <summary>
		/// Run a query and return multiple items with paging
		/// </summary>
		/// <typeparam name="T">Return value type</typeparam>
		/// <param name="db">IWpDb</param>
		/// <param name="w">IUnitOfWork</param>
		/// <param name="page">Page number</param>
		/// <param name="opt">Function to return query options</param>
		/// <param name="filters">Optional content filters to apply</param>
		Task<Option<IPagedList<T>>> ExecuteAsync<T>(
			IWpDb db,
			IUnitOfWork w,
			ulong page,
			Query.GetPostsOptions opt,
			params IContentFilter[] filters
		)
			where T : IWithId<WpPostId>;
	}
}
