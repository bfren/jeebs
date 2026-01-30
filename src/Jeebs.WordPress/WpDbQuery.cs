// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data.Common;
using Jeebs.Logging;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Functions;

namespace Jeebs.WordPress;

/// <inheritdoc cref="IWpDbQuery"/>
internal sealed class WpDbQuery : DbQuery<IWpDb>, IWpDbQuery
{
	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="db">IWpDb.</param>
	/// <param name="log">ILog.</param>
	internal WpDbQuery(IWpDb db, ILog<IWpDbQuery> log) : base(db, log) { }

	/// <inheritdoc/>
	public async Task<Result<IEnumerable<T>>> AttachmentsAsync<T>(Query.GetAttachmentsOptions opt)
		where T : IPostAttachment
	{
		using var w = await Db.StartWorkAsync();
		return await QueryAttachmentsF.ExecuteAsync<T>(Db, w, opt);
	}

	/// <inheritdoc/>
	public async Task<Result<string>> AttachmentFilePathAsync(WpPostId fileId)
	{
		using var w = await Db.StartWorkAsync();
		return await QueryAttachmentsF.GetFilePathAsync(Db, w, fileId);
	}

	/// <inheritdoc/>
	public async Task<Result<IEnumerable<T>>> PostsAsync<T>(Query.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId, ulong>
	{
		using var w = await Db.StartWorkAsync();
		return await QueryPostsF.ExecuteAsync<T>(Db, w, opt, filters);
	}

	/// <inheritdoc/>
	public async Task<Result<IPagedList<T>>> PostsAsync<T>(ulong page, Query.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId, ulong>
	{
		using var w = await Db.StartWorkAsync();
		return await QueryPostsF.ExecuteAsync<T>(Db, w, page, opt, filters);
	}

	/// <inheritdoc/>
	public async Task<Result<(WpPostId? prev, WpPostId? next)>> PreviousAndNextPostsAsync(WpPostId id, Query.GetPostsOptions opt)
	{
		using var w = await Db.StartWorkAsync();
		return await QueryPostsF.GetPreviousAndNextAsync(Db, w, id, opt);
	}

	/// <inheritdoc/>
	public async Task<Result<IEnumerable<T>>> PostsMetaAsync<T>(Query.GetPostsMetaOptions opt)
		where T : IWithId<WpPostMetaId, ulong>
	{
		using var w = await Db.StartWorkAsync();
		return await QueryPostsMetaF.ExecuteAsync<T>(Db, w, opt);
	}

	/// <inheritdoc/>
	public async Task<Result<IEnumerable<T>>> PostsTaxonomyAsync<T>(Query.GetPostsTaxonomyOptions opt)
		where T : IWithId<WpTermId, ulong>
	{
		using var w = await Db.StartWorkAsync();
		return await QueryPostsTaxonomyF.ExecuteAsync<T>(Db, w, opt);
	}

	/// <inheritdoc/>
	public async Task<Result<IEnumerable<T>>> TermsAsync<T>(Query.GetTermsOptions opt)
		where T : IWithId<WpTermId, ulong>
	{
		using var w = await Db.StartWorkAsync();
		return await QueryTermsF.ExecuteAsync<T>(Db, w, opt);
	}
}
