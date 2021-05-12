// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data
{
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
