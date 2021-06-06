// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
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
}
