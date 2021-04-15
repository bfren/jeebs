// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
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
		/// Search text comparison (should normally be <see cref="Compare.Equal"/> or <see cref="Compare.Like"/>)
		/// </summary>
		Compare SearchComparison { get; init; }

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
		long? ParentId { get; init; }

		/// <summary>
		/// Search post taxonomies
		/// </summary>
		IImmutableList<(Taxonomy taxonomy, long id)> Taxonomies { get; init; }

		/// <summary>
		/// Search custom fields
		/// </summary>
		IImmutableList<(ICustomField field, Compare cmp, object value)> CustomFields { get; init; }
	}
}
