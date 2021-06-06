// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data
{
	/// <inheritdoc cref="IQueryPartsBuilder{TId}"/>
	public interface IQueryTermsPartsBuilder : IQueryPartsBuilder<WpTermId>
	{
		/// <summary>
		/// Add Where Taxonomy
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="taxonomy">Taxonomy</param>
		Option<QueryParts> AddWhereTaxonomy(QueryParts parts, Taxonomy? taxonomy);

		/// <summary>
		/// Add Where Slug
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="slug">Term Slug</param>
		Option<QueryParts> AddWhereSlug(QueryParts parts, string? slug);

		/// <summary>
		/// Add Where Count
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="countAtLeast">Terms with at least this many posts</param>
		Option<QueryParts> AddWhereCount(QueryParts parts, long countAtLeast);
	}
}
