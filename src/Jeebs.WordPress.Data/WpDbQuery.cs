// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using F.WordPressF.DataF;
using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	/// <inheritdoc cref="IWpDbQuery"/>
	internal sealed class WpDbQuery : DbQuery<IWpDb>, IWpDbQuery
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">IWpDb</param>
		/// <param name="log">ILog</param>
		internal WpDbQuery(IWpDb db, ILog<IWpDbQuery> log) : base(db, log) { }

		/// <inheritdoc/>
		public Task<Option<IEnumerable<T>>> PostsAsync<T>(Query.GetPostsOptions opt, params IContentFilter[] filters)
			where T : IWithId<WpPostId> =>
			QueryPostsF.ExecuteAsync<T>(Db, opt, filters);

		/// <inheritdoc/>
		public Task<Option<IPagedList<T>>> PostsAsync<T>(long page, Query.GetPostsOptions opt, params IContentFilter[] filters)
			where T : IWithId<WpPostId> =>
			QueryPostsF.ExecuteAsync<T>(Db, page, opt, filters);

		/// <inheritdoc/>
		public Task<Option<IEnumerable<T>>> PostsMetaAsync<T>(Query.GetPostsMetaOptions opt)
			where T : IWithId<WpPostMetaId> =>
			QueryPostsMetaF.ExecuteAsync<T>(Db, opt);

		/// <inheritdoc/>
		public Task<Option<IEnumerable<T>>> PostsTaxonomyAsync<T>(Query.GetPostsTaxonomyOptions opt)
			where T : IWithId<WpTermId> =>
			QueryPostsTaxonomyF.ExecuteAsync<T>(Db, opt);

		/// <inheritdoc/>
		public Task<Option<IEnumerable<T>>> TermsAsync<T>(Query.GetTermsOptions opt)
			where T : IWithId<WpTermId> =>
			QueryTermsF.ExecuteAsync<T>(Db, opt);
	}
}
