// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Querying;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Query Posts
	/// </summary>
	/// <typeparam name="TEntity">Post Entity type</typeparam>
	public interface IQueryPosts<TEntity>
		where TEntity : WpPostEntity
	{
		/// <summary>
		/// Run a query and return multiple items
		/// </summary>
		/// <typeparam name="TModel">Return value type</typeparam>
		/// <param name="opt">Function to return query options</param>
		Task<Option<IEnumerable<TModel>>> ExecuteAsync<TModel>(
			Func<QueryPostsOptions<TEntity>, QueryPostsOptions<TEntity>> opt
		);

		/// <summary>
		/// Run a query and return multiple items
		/// </summary>
		/// <typeparam name="TModel">Return value type</typeparam>
		/// <param name="page">Page number</param>
		/// <param name="opt">Function to return query options</param>
		Task<Option<IPagedList<TModel>>> ExecuteAsync<TModel>(
			long page,
			Func<QueryPostsOptions<TEntity>, QueryPostsOptions<TEntity>> opt
		);
	}
}
