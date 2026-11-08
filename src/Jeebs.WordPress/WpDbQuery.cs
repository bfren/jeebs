// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Logging;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Functions;

namespace Jeebs.WordPress;

/// <inheritdoc cref="IWpDbQuery"/>
internal sealed class WpDbQuery : IWpDbQuery
{
	private IWpDb Db { get; init; }

	internal IWpDb DbTest =>
		Db;

	private ILog Log { get; init; }

	internal ILog LogTest =>
		Log;

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="db">IWpDb.</param>
	/// <param name="log">ILog.</param>
	internal WpDbQuery(IWpDb db, ILog<IWpDbQuery> log) =>
		(Db, Log) = (db, log);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> AttachmentsAsync<T>(Query.GetAttachmentsOptions opt)
		where T : IPostAttachment =>
		Db.DoWorkAsync(
			unitOfWork => QueryAttachmentsF.ExecuteAsync<T>(Db, unitOfWork, opt)
		);

	/// <inheritdoc/>
	public Task<Result<string>> AttachmentFilePathAsync(WpPostId fileId) =>
		Db.DoWorkAsync(
			unitOfWork => QueryAttachmentsF.GetFilePathAsync(Db, unitOfWork, fileId)
		);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> PostsAsync<T>(Query.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId, ulong> =>
		Db.DoWorkAsync(
			unitOfWork => QueryPostsF.ExecuteAsync<T>(Db, unitOfWork, opt, filters)
		);

	/// <inheritdoc/>
	public Task<Result<IPagedList<T>>> PostsAsync<T>(ulong page, Query.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId, ulong> =>
		Db.DoWorkAsync(
			unitOfWork => QueryPostsF.ExecuteAsync<T>(Db, unitOfWork, page, opt, filters)
		);

	/// <inheritdoc/>
	public Task<Result<(WpPostId? prev, WpPostId? next)>> PreviousAndNextPostsAsync(WpPostId id, Query.GetPostsOptions opt) =>
		Db.DoWorkAsync(
			unitOfWork => QueryPostsF.GetPreviousAndNextAsync(Db, unitOfWork, id, opt)
		);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> PostsMetaAsync<T>(Query.GetPostsMetaOptions opt)
		where T : IWithId<WpPostMetaId, ulong> =>
		Db.DoWorkAsync(
			unitOfWork => QueryPostsMetaF.ExecuteAsync<T>(Db, unitOfWork, opt)
		);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> PostsTaxonomyAsync<T>(Query.GetPostsTaxonomyOptions opt)
		where T : IWithId<WpTermId, ulong> =>
		Db.DoWorkAsync(
			unitOfWork => QueryPostsTaxonomyF.ExecuteAsync<T>(Db, unitOfWork, opt)
		);

	/// <inheritdoc/>
	public Task<Result<IEnumerable<T>>> TermsAsync<T>(Query.GetTermsOptions opt)
		where T : IWithId<WpTermId, ulong> =>
		Db.DoWorkAsync(
			unitOfWork => QueryTermsF.ExecuteAsync<T>(Db, unitOfWork, opt)
		);
}
