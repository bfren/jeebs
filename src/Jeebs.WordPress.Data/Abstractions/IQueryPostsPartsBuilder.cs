// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data
{
	/// <inheritdoc cref="IQueryPartsBuilder{TId}"/>
	public interface IQueryPostsPartsBuilder : IQueryPartsBuilder<WpPostId>
	{
		/// <summary>
		/// Add Where Post Type
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="type">Post Type</param>
		Option<QueryParts> AddWhereType(QueryParts parts, PostType type);

		/// <summary>
		/// Add Where Post Status
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="status">Post Status</param>
		Option<QueryParts> AddWhereStatus(QueryParts parts, PostStatus status);

		/// <summary>
		/// Add Where Search
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="fields">Fields to search</param>
		/// <param name="cmp">Search Comparison</param>
		/// <param name="text">Search text</param>
		Option<QueryParts> AddWhereSearch(QueryParts parts, SearchPostFields fields, Compare cmp, string? text);

		/// <summary>
		/// Add Where From / To
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="from">Published Start date</param>
		Option<QueryParts> AddWherePublishedFrom(QueryParts parts, DateTime? from);

		/// <summary>
		/// Add Where From / To
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="to">Published End date</param>
		Option<QueryParts> AddWherePublishedTo(QueryParts parts, DateTime? to);

		/// <summary>
		/// Add Where Parent ID
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="parentId">Parent ID</param>
		Option<QueryParts> AddWhereParentId(QueryParts parts, long? parentId);

		/// <summary>
		/// Add Where Taxonomies
		/// </summary>
		/// <param name="parts">QueryParts</param>
		Option<QueryParts> AddWhereTaxonomies(QueryParts parts, IImmutableList<(Taxonomy taxonomy, long id)> taxonomies);

		/// <summary>
		/// Add Where Custom Fields
		/// </summary>
		/// <param name="parts">QueryParts</param>
		Option<QueryParts> AddWhereCustomFields(QueryParts parts, IImmutableList<(ICustomField, Compare, object)> customFields);
	}
}
