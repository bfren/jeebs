// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.WordPress.ContentFilters;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.Ids;

namespace Jeebs.WordPress;

/// <summary>
/// WordPress Database queries.
/// </summary>
public interface IWpDbQuery : IDbQuery
{
	/// <summary>
	/// Get Attachments matching the specified options.
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="opt">Function to return query options.</param>
	Task<Result<IEnumerable<T>>> AttachmentsAsync<T>(Query.GetAttachmentsOptions opt)
		where T : IPostAttachment;

	/// <summary>
	/// Get filesystem path of specified Attachment.
	/// </summary>
	/// <param name="fileId">Attachment ID.</param>
	Task<Result<string>> AttachmentFilePathAsync(WpPostId fileId);

	/// <summary>
	/// Get Posts matching the specified options.
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="opt">Function to return query options.</param>
	/// <param name="filters">Optional content filters to apply.</param>
	Task<Result<IEnumerable<T>>> PostsAsync<T>(Query.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId, ulong>;

	/// <summary>
	/// Get Posts matching the specified options, with paging.
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="page">Page number.</param>
	/// <param name="opt">Function to return query options.</param>
	/// <param name="filters">Optional content filters to apply.</param>
	Task<Result<IPagedList<T>>> PostsAsync<T>(ulong page, Query.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId, ulong>;

	/// <summary>
	/// Get the Previous and Next posts matching the current query.
	/// </summary>
	/// <param name="id">Current Post ID.</param>
	/// <param name="opt">Function to return query options.</param>
	Task<Result<(WpPostId? prev, WpPostId? next)>> PreviousAndNextPostsAsync(WpPostId id, Query.GetPostsOptions opt);

	/// <summary>
	/// Get Posts Meta matching the specified options.
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="opt">Function to return query options.</param>
	Task<Result<IEnumerable<T>>> PostsMetaAsync<T>(Query.GetPostsMetaOptions opt)
		where T : IWithId<WpPostMetaId, ulong>;

	/// <summary>
	/// Get Posts Taxonomy matching the specified options.
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="opt">Function to return query options.</param>
	Task<Result<IEnumerable<T>>> PostsTaxonomyAsync<T>(Query.GetPostsTaxonomyOptions opt)
		where T : IWithId<WpTermId, ulong>;

	/// <summary>
	/// Get Terms matching the specified options.
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="opt">Function to return query options.</param>
	Task<Result<IEnumerable<T>>> TermsAsync<T>(Query.GetTermsOptions opt)
		where T : IWithId<WpTermId, ulong>;
}
