// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using F.WordPressF.DataF;
using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data;

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
	public async Task<Maybe<IEnumerable<T>>> AttachmentsAsync<T>(Querying.GetAttachmentsOptions opt)
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
	public async Task<Maybe<IEnumerable<T>>> PostsAsync<T>(Querying.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId>
	{
		using var w = Db.UnitOfWork;
		return await QueryPostsF.ExecuteAsync<T>(Db, w, opt, filters).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<IPagedList<T>>> PostsAsync<T>(ulong page, Querying.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId>
	{
		using var w = Db.UnitOfWork;
		return await QueryPostsF.ExecuteAsync<T>(Db, w, page, opt, filters).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<(WpPostId? prev, WpPostId? next)>> PreviousAndNextPostsAsync(WpPostId id, Querying.GetPostsOptions opt)
	{
		using var w = Db.UnitOfWork;
		return await QueryPostsF.GetPreviousAndNextAsync(Db, w, id, opt).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<IEnumerable<T>>> PostsMetaAsync<T>(Querying.GetPostsMetaOptions opt)
		where T : IWithId<WpPostMetaId>
	{
		using var w = Db.UnitOfWork;
		return await QueryPostsMetaF.ExecuteAsync<T>(Db, w, opt).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<IEnumerable<T>>> PostsTaxonomyAsync<T>(Querying.GetPostsTaxonomyOptions opt)
		where T : IWithId<WpTermId>
	{
		using var w = Db.UnitOfWork;
		return await QueryPostsTaxonomyF.ExecuteAsync<T>(Db, w, opt).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<IEnumerable<T>>> TermsAsync<T>(Querying.GetTermsOptions opt)
		where T : IWithId<WpTermId>
	{
		using var w = Db.UnitOfWork;
		return await QueryTermsF.ExecuteAsync<T>(Db, w, opt).ConfigureAwait(false);
	}
}
