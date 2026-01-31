// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Query;
using Jeebs.WordPress.CustomFields;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="IQueryPartsBuilder{TId}"/>
public interface IQueryPostsPartsBuilder : IQueryPartsBuilder<WpPostId>
{
	/// <summary>
	/// Add Where Post Type.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="type">Post Type.</param>
	Result<QueryParts> AddWhereType(QueryParts parts, PostType type);

	/// <summary>
	/// Add Where Post Status.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="status">Post Status.</param>
	Result<QueryParts> AddWhereStatus(QueryParts parts, PostStatus status);

	/// <summary>
	/// Add Where Search.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="fields">Fields to search.</param>
	/// <param name="cmp">Search Comparison.</param>
	/// <param name="text">Search text.</param>
	Result<QueryParts> AddWhereSearch(QueryParts parts, SearchPostField fields, Compare cmp, string? text);

	/// <summary>
	/// Add Where From / To.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="fromDate">Published Start date.</param>
	Result<QueryParts> AddWherePublishedFrom(QueryParts parts, DateTime? fromDate);

	/// <summary>
	/// Add Where From / To.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="toDate">Published End date.</param>
	Result<QueryParts> AddWherePublishedTo(QueryParts parts, DateTime? toDate);

	/// <summary>
	/// Add Where Parent ID.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="parentId">Parent ID.</param>
	Result<QueryParts> AddWhereParentId(QueryParts parts, WpPostId? parentId);

	/// <summary>
	/// Add Where Taxonomies.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="taxonomies">Taxonomies.</param>
	Result<QueryParts> AddWhereTaxonomies(QueryParts parts, IImmutableList<(Taxonomy taxonomy, WpTermId id)> taxonomies);

	/// <summary>
	/// Add Where Custom Fields.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="customFields">Custom Fields.</param>
	Result<QueryParts> AddWhereCustomFields(QueryParts parts, IImmutableList<(ICustomField, Compare, object)> customFields);
}
