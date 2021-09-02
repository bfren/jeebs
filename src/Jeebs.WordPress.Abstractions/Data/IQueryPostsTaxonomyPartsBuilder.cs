// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data;

/// <inheritdoc cref="IQueryPartsBuilder{TId}"/>
public interface IQueryPostsTaxonomyPartsBuilder : IQueryPartsBuilder<WpTermId>
{
	/// <summary>
	/// Add Where Taxonomies
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="taxonomies">Taxonomies</param>
	Option<QueryParts> AddWhereTaxonomies(QueryParts parts, IImmutableList<Taxonomy> taxonomies);

	/// <summary>
	/// Add Where Post IDs
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="postIds">Post IDs</param>
	Option<QueryParts> AddWherePostIds(QueryParts parts, IImmutableList<WpPostId> postIds);

	/// <summary>
	/// Add custom Sort or default Sort
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="sortRandom">Whether or not to sort randomly</param>
	/// <param name="sort">Sort columns</param>
	/// <param name="sortBy">Taxonomy sort</param>
	Option<QueryParts> AddSort(QueryParts parts, bool sortRandom, IImmutableList<(IColumn, SortOrder)> sort, TaxonomySort sortBy);
}
