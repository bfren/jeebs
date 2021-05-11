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
	public interface IQueryPostsTaxonomyOptions : IQueryOptions<WpTermId>
	{
		/// <summary>
		/// The taxonomies to get
		/// </summary>
		IImmutableList<Taxonomy> Taxonomies { get; init; }

		/// <summary>
		/// Get taxonomies for specific Posts
		/// </summary>
		IImmutableList<WpPostId> PostIds { get; init; }

		/// <summary>
		/// Sort order for Taxonomy terms - will be ignored if <see cref="IQueryOptions{TId}.Sort"/>
		/// or <see cref="IQueryOptions{TId}.SortRandom"/> are used
		/// </summary>
		TaxonomySort SortBy { get; init; }
	}
}
