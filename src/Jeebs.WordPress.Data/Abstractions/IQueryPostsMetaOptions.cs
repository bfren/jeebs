// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Query Posts Meta Options
	/// </summary>
	/// <typeparam name="TEntity">Post Meta Entity type</typeparam>
	public interface IQueryPostsMetaOptions<TEntity> : IQueryOptions<TEntity, WpPostMetaId>
		where TEntity : WpPostMetaEntity
	{
		/// <summary>
		/// Get meta for a single Post
		/// </summary>
		long? PostId { get; init; }

		/// <summary>
		/// Get meta for multiple Posts
		/// </summary>
		IImmutableList<long>? PostIds { get; init; }

		/// <summary>
		/// Only return a single Key
		/// </summary>
		string? Key { get; init; }
	}
}
