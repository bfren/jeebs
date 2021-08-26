// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Query Posts Options
	/// </summary>
	public interface IQueryPostsOptions : IQueryOptions<WpPostId>
	{
		/// <summary>
		/// Search Post type - default is <see cref="PostType.Post"/>
		/// </summary>
		PostType Type { get; init; }

		/// <summary>
		/// Search Post status - default is <see cref="PostStatus.Publish"/>
		/// </summary>
		PostStatus Status { get; init; }

		/// <summary>
		/// Search Post Text
		/// </summary>
		string? SearchText { get; init; }

		/// <summary>
		/// Search text fields
		/// </summary>
		SearchPostField SearchFields { get; init; }

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
		WpPostId? ParentId { get; init; }

		/// <summary>
		/// Search post taxonomies
		/// </summary>
		IImmutableList<(Taxonomy taxonomy, WpTermId id)> Taxonomies { get; init; }

		/// <summary>
		/// Search custom fields
		/// </summary>
		IImmutableList<(ICustomField field, Compare cmp, object value)> CustomFields { get; init; }
	}
}
