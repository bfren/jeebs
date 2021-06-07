// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// WordPress Database queries
	/// </summary>
	public interface IWpDbQuery : IDbQuery
	{
		/// <summary>
		/// Get Posts matching the specified options
		/// </summary>
		/// <typeparam name="T">Return Model type</typeparam>
		/// <param name="opt">Function to return query options</param>
		/// <param name="filters">Optional content filters to apply</param>
		Task<Option<IEnumerable<T>>> PostsAsync<T>(Query.GetPostsOptions opt, params IContentFilter[] filters)
			where T : IWithId<WpPostId>;

		/// <summary>
		/// Get Posts matching the specified options, with paging
		/// </summary>
		/// <typeparam name="T">Return Model type</typeparam>
		/// <param name="page">Page number</param>
		/// <param name="opt">Function to return query options</param>
		/// <param name="filters">Optional content filters to apply</param>
		Task<Option<IPagedList<T>>> PostsAsync<T>(long page, Query.GetPostsOptions opt, params IContentFilter[] filters)
			where T : IWithId<WpPostId>;

		/// <summary>
		/// Get Posts Meta matching the specified options
		/// </summary>
		/// <typeparam name="T">Return Model type</typeparam>
		/// <param name="opt">Function to return query options</param>
		Task<Option<IEnumerable<T>>> PostsMetaAsync<T>(Query.GetPostsMetaOptions opt)
			where T : IWithId<WpPostMetaId>;

		/// <summary>
		/// Get Posts Taxonomy matching the specified options
		/// </summary>
		/// <typeparam name="T">Return Model type</typeparam>
		/// <param name="opt">Function to return query options</param>
		Task<Option<IEnumerable<T>>> PostsTaxonomyAsync<T>(Query.GetPostsTaxonomyOptions opt)
			where T : IWithId<WpTermId>;

		/// <summary>
		/// Get Terms matching the specified options
		/// </summary>
		/// <typeparam name="T">Return Model type</typeparam>
		/// <param name="opt">Function to return query options</param>
		Task<Option<IEnumerable<T>>> TermsAsync<T>(Query.GetTermsOptions opt)
			where T : IWithId<WpTermId>;
	}
}
