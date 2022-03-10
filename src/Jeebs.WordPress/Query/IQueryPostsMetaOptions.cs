// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Query;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress;

/// <summary>
/// Query Posts Meta Options
/// </summary>
public interface IQueryPostsMetaOptions : IQueryOptions<WpPostMetaId>
{
	/// <summary>
	/// Get meta for a single Post
	/// </summary>
	WpPostId? PostId { get; init; }

	/// <summary>
	/// Get meta for multiple Posts
	/// </summary>
	IImmutableList<WpPostId> PostIds { get; init; }

	/// <summary>
	/// Only return a single Key
	/// </summary>
	string? Key { get; init; }
}
