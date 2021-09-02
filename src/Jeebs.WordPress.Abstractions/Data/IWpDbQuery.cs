// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data;

/// <summary>
/// WordPress Database queries
/// </summary>
public interface IWpDbQuery : IDbQuery
{
	/// <summary>
	/// Get Attachments matching the specified options
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="opt">Function to return query options</param>
	Task<Option<IEnumerable<T>>> AttachmentsAsync<T>(Querying.GetAttachmentsOptions opt)
		where T : IPostAttachment;

	/// <summary>
	/// Get filesystem path of specified Attachment
	/// </summary>
	/// <param name="fileId">Attachment ID</param>
	Task<Option<string>> AttachmentFilePathAsync(WpPostId fileId);

	/// <summary>
	/// Get Posts matching the specified options
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="opt">Function to return query options</param>
	/// <param name="filters">Optional content filters to apply</param>
	Task<Option<IEnumerable<T>>> PostsAsync<T>(Querying.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId>;

	/// <summary>
	/// Get Posts matching the specified options, with paging
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="page">Page number</param>
	/// <param name="opt">Function to return query options</param>
	/// <param name="filters">Optional content filters to apply</param>
	Task<Option<IPagedList<T>>> PostsAsync<T>(ulong page, Querying.GetPostsOptions opt, params IContentFilter[] filters)
		where T : IWithId<WpPostId>;

	/// <summary>
	/// Get the Previous and Next posts matching the current query
	/// </summary>
	/// <param name="id">Current Post ID</param>
	/// <param name="opt">Function to return query options</param>
	Task<Option<(WpPostId? prev, WpPostId? next)>> PreviousAndNextPostsAsync(WpPostId id, Querying.GetPostsOptions opt);

	/// <summary>
	/// Get Posts Meta matching the specified options
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="opt">Function to return query options</param>
	Task<Option<IEnumerable<T>>> PostsMetaAsync<T>(Querying.GetPostsMetaOptions opt)
		where T : IWithId<WpPostMetaId>;

	/// <summary>
	/// Get Posts Taxonomy matching the specified options
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="opt">Function to return query options</param>
	Task<Option<IEnumerable<T>>> PostsTaxonomyAsync<T>(Querying.GetPostsTaxonomyOptions opt)
		where T : IWithId<WpTermId>;

	/// <summary>
	/// Get Terms matching the specified options
	/// </summary>
	/// <typeparam name="T">Return Model type</typeparam>
	/// <param name="opt">Function to return query options</param>
	Task<Option<IEnumerable<T>>> TermsAsync<T>(Querying.GetTermsOptions opt)
		where T : IWithId<WpTermId>;
}
