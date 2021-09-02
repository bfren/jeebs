// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	/// <inheritdoc cref="IQueryPartsBuilder{TId}"/>
	public interface IQueryPostsMetaPartsBuilder : IQueryPartsBuilder<WpPostMetaId>
	{
		/// <summary>
		/// Add Where Post ID
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="postId">Post ID</param>
		/// <param name="postIds">Post IDs</param>
		Option<QueryParts> AddWherePostId(QueryParts parts, WpPostId? postId, IImmutableList<WpPostId> postIds);

		/// <summary>
		/// Add Where Post Status
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="Key">Meta Key</param>
		Option<QueryParts> AddWhereKey(QueryParts parts, string? Key);
	}
}
