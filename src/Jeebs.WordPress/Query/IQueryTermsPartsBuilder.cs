// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;
using MaybeF;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="IQueryPartsBuilder{TId}"/>
public interface IQueryTermsPartsBuilder : IQueryPartsBuilder<WpTermId>
{
	/// <summary>
	/// Add Where Taxonomy
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="taxonomy">Taxonomy</param>
	Maybe<QueryParts> AddWhereTaxonomy(QueryParts parts, Taxonomy? taxonomy);

	/// <summary>
	/// Add Where Slug
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="slug">Term Slug</param>
	Maybe<QueryParts> AddWhereSlug(QueryParts parts, string? slug);

	/// <summary>
	/// Add Where Count
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="countAtLeast">Terms with at least this many posts</param>
	Maybe<QueryParts> AddWhereCount(QueryParts parts, long countAtLeast);
}
