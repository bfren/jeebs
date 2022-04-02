// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.Logging;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Functions;
using StrongId;

namespace Jeebs.WordPress;

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
	public async Task<Maybe<IEnumerable<T>>> AttachmentsAsync<T>(Query.GetAttachmentsOptions opt)
		where T : IPostAttachment
	{
		using var w = Db.UnitOfWork;
		return await QueryAttachmentsF.ExecuteAsync<T>(Db, w, opt).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<string>> AttachmentFilePathAsync(WpPostId fileId)
	{
		using var w = Db.UnitOfWork;
		return await QueryAttachmentsF.GetFilePathAsync(Db, w, fileId).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<IEnumerable<T>>> PostsAsync<T>(Query.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId>
	{
		using var w = Db.UnitOfWork;
		return await QueryPostsF.ExecuteAsync<T>(Db, w, opt, filters).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<IPagedList<T>>> PostsAsync<T>(ulong page, Query.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId>
	{
		using var w = Db.UnitOfWork;
		return await QueryPostsF.ExecuteAsync<T>(Db, w, page, opt, filters).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<(WpPostId? prev, WpPostId? next)>> PreviousAndNextPostsAsync(WpPostId id, Query.GetPostsOptions opt)
	{
		using var w = Db.UnitOfWork;
		return await QueryPostsF.GetPreviousAndNextAsync(Db, w, id, opt).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<IEnumerable<T>>> PostsMetaAsync<T>(Query.GetPostsMetaOptions opt)
		where T : IWithId<WpPostMetaId>
	{
		using var w = Db.UnitOfWork;
		return await QueryPostsMetaF.ExecuteAsync<T>(Db, w, opt).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<IEnumerable<T>>> PostsTaxonomyAsync<T>(Query.GetPostsTaxonomyOptions opt)
		where T : IWithId<WpTermId>
	{
		using var w = Db.UnitOfWork;
		return await QueryPostsTaxonomyF.ExecuteAsync<T>(Db, w, opt).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<IEnumerable<T>>> TermsAsync<T>(Query.GetTermsOptions opt)
		where T : IWithId<WpTermId>
	{
		using var w = Db.UnitOfWork;
		return await QueryTermsF.ExecuteAsync<T>(Db, w, opt).ConfigureAwait(false);
	}
}
