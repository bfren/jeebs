// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Query Posts Options
	/// </summary>
	/// <typeparam name="TEntity">Post Entity type</typeparam>
	public interface IQueryPostsOptions<TEntity> : IQueryOptions<TEntity, WpPostId>
		where TEntity : WpPostEntity
	{
		/// <summary>
		/// Search Post type
		/// </summary>
		PostType Type { get; init; }

		/// <summary>
		/// Search Post status
		/// </summary>
		PostStatus Status { get; init; }

		/// <summary>
		/// Search Post Text
		/// </summary>
		string? SearchText { get; init; }

		/// <summary>
		/// Search text fields
		/// </summary>
		SearchPostFields SearchFields { get; init; }

		/// <summary>
		/// Search text operator (= or LIKE)
		/// </summary>
		SearchOperator SearchOperator { get; init; }

		/// <summary>
		/// Search Post published from
		/// </summary>
		DateTime? From { get; init; }

		/// <summary>
		/// Search Post published to
		/// </summary>
		DateTime? To { get; init; }

		/// <summary>
		/// Parent ID
		/// </summary>
		int? ParentId { get; init; }

		/// <summary>
		/// Search post taxonomies
		/// </summary>
		List<(Taxonomy taxonomy, long id)> Taxonomies { get; init; }

		/// <summary>
		/// Search custom fields
		/// </summary>
		List<(ICustomField field, SearchOperator op, object value)> CustomFields { get; init; }
	}
}
