// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.QueryBuilder;
using Jeebs.WordPress.Entities.Ids;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="IQueryPartsBuilder{TId}"/>
public interface IQueryPostsMetaPartsBuilder : IQueryPartsBuilder<WpPostMetaId>
{
	/// <summary>
	/// Add Where Post ID.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="postId">Post ID.</param>
	/// <param name="postIds">Post IDs.</param>
	Result<QueryParts> AddWherePostId(QueryParts parts, WpPostId? postId, IImmutableList<WpPostId> postIds);

	/// <summary>
	/// Add Where Post Status.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="key">Meta Key.</param>
	Result<QueryParts> AddWhereKey(QueryParts parts, string? key);
}
