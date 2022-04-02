// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="IQueryPartsBuilder{TId}"/>
public interface IQueryPostsTaxonomyPartsBuilder : IQueryPartsBuilder<WpTermId>
{
	/// <summary>
	/// Add Where Taxonomies
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="taxonomies">Taxonomies</param>
	Maybe<QueryParts> AddWhereTaxonomies(QueryParts parts, IImmutableList<Taxonomy> taxonomies);

	/// <summary>
	/// Add Where Post IDs
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="postIds">Post IDs</param>
	Maybe<QueryParts> AddWherePostIds(QueryParts parts, IImmutableList<WpPostId> postIds);

	/// <summary>
	/// Add custom Sort or default Sort
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="sortRandom">Whether or not to sort randomly</param>
	/// <param name="sort">Sort columns</param>
	/// <param name="sortBy">Taxonomy sort</param>
	Maybe<QueryParts> AddSort(QueryParts parts, bool sortRandom, IImmutableList<(IColumn, SortOrder)> sort, TaxonomySort sortBy);
}
