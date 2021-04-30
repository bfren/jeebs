// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Query Posts Taxonomy Options
	/// </summary>
	/// <typeparam name="TEntity">Term Entity type</typeparam>
	public interface IQueryPostsTaxonomyOptions<TEntity> : IQueryOptions<TEntity, WpTermId>
		where TEntity : WpTermEntity
	{
		/// <summary>
		/// The taxonomies to get
		/// </summary>
		IImmutableList<Taxonomy>? Taxonomies { get; init; }

		/// <summary>
		/// Get taxonomies for specific Posts
		/// </summary>
		IImmutableList<long>? PostIds { get; init; }
	}
}
