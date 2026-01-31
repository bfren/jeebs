// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="IQueryPartsBuilder{TId}"/>
public interface IQueryTermsPartsBuilder : IQueryPartsBuilder<WpTermId>
{
	/// <summary>
	/// Add Where Taxonomy.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="taxonomy">Taxonomy.</param>
	Result<QueryParts> AddWhereTaxonomy(QueryParts parts, Taxonomy? taxonomy);

	/// <summary>
	/// Add Where Slug.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="slug">Term Slug.</param>
	Result<QueryParts> AddWhereSlug(QueryParts parts, string? slug);

	/// <summary>
	/// Add Where Count.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="countAtLeast">Terms with at least this many posts.</param>
	Result<QueryParts> AddWhereCount(QueryParts parts, long countAtLeast);
}
