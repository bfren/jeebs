// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Query;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.Query.Options;

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
