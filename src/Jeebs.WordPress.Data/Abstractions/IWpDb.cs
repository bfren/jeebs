// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// WordPress Database instance
	/// </summary>
	public interface IWpDb : IDb
	{
		/// <inheritdoc cref="IWpDbQuery"/>
		IWpDbQuery Query { get; }

		/// <inheritdoc cref="IWpDbSchema"/>
		IWpDbSchema Schema { get; }

		#region Query Methods

		/// <inheritdoc cref="QueryPostsAsync{TModel}(long, Query.GetPostsOptions,IContentFilter[])"/>
		Task<Option<IEnumerable<TModel>>> QueryPostsAsync<TModel>(Query.GetPostsOptions opt, params IContentFilter[] filters)
			where TModel : IWithId<WpPostId>;

		/// <summary>
		/// Query Post objects
		/// </summary>
		/// <param name="page">Page number</param>
		/// <param name="opt">Query options</param>
		/// <param name="filters">Optional content filters</param>
		Task<Option<IPagedList<TModel>>> QueryPostsAsync<TModel>(long page, Query.GetPostsOptions opt, params IContentFilter[] filters)
			where TModel : IWithId<WpPostId>;

		/// <summary>
		/// Query Terms
		/// </summary>
		/// <param name="opt">Query options</param>
		Task<Option<IEnumerable<TModel>>> QueryTermsAsync<TModel>(Query.GetTermsOptions opt)
			where TModel : IWithId<WpTermId>;

		#endregion
	}
}
