// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Query;
using Jeebs.WordPress.Data.CustomFields;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using Maybe;

namespace Jeebs.WordPress.Data;

/// <inheritdoc cref="IQueryPartsBuilder{TId}"/>
public interface IQueryPostsPartsBuilder : IQueryPartsBuilder<WpPostId>
{
	/// <summary>
	/// Add Where Post Type
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="type">Post Type</param>
	Maybe<QueryParts> AddWhereType(QueryParts parts, PostType type);

	/// <summary>
	/// Add Where Post Status
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="status">Post Status</param>
	Maybe<QueryParts> AddWhereStatus(QueryParts parts, PostStatus status);

	/// <summary>
	/// Add Where Search
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="fields">Fields to search</param>
	/// <param name="cmp">Search Comparison</param>
	/// <param name="text">Search text</param>
	Maybe<QueryParts> AddWhereSearch(QueryParts parts, SearchPostField fields, Compare cmp, string? text);

	/// <summary>
	/// Add Where From / To
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="fromDate">Published Start date</param>
	Maybe<QueryParts> AddWherePublishedFrom(QueryParts parts, DateTime? fromDate);

	/// <summary>
	/// Add Where From / To
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="toDate">Published End date</param>
	Maybe<QueryParts> AddWherePublishedTo(QueryParts parts, DateTime? toDate);

	/// <summary>
	/// Add Where Parent ID
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="parentId">Parent ID</param>
	Maybe<QueryParts> AddWhereParentId(QueryParts parts, WpPostId? parentId);

	/// <summary>
	/// Add Where Taxonomies
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="taxonomies">Taxonomies</param>
	Maybe<QueryParts> AddWhereTaxonomies(QueryParts parts, IImmutableList<(Taxonomy taxonomy, WpTermId id)> taxonomies);

	/// <summary>
	/// Add Where Custom Fields
	/// </summary>
	/// <param name="parts">QueryParts</param>
	/// <param name="customFields">Custom Fields</param>
	Maybe<QueryParts> AddWhereCustomFields(QueryParts parts, IImmutableList<(ICustomField, Compare, object)> customFields);
}
