// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.Querying;

/// <inheritdoc cref="IQueryPostsOptions"/>
public abstract record class PostsOptions : Options<WpPostId>, IQueryPostsOptions
{
	/// <summary>
	/// IQueryPostsPartsBuilder
	/// </summary>
	protected new IQueryPostsPartsBuilder Builder =>
		(IQueryPostsPartsBuilder)base.Builder;

	/// <inheritdoc/>
	public PostType Type { get; init; } = PostType.Post;

	/// <inheritdoc/>
	public PostStatus Status { get; init; } = PostStatus.Publish;

	/// <inheritdoc/>
	public string? SearchText { get; init; }

	/// <inheritdoc/>
	public SearchPostField SearchFields { get; init; } = SearchPostField.All;

	/// <inheritdoc/>
	public Compare SearchComparison { get; init; } = Compare.Like;

	/// <inheritdoc/>
	public DateTime? From { get; init; }

	/// <inheritdoc/>
	public DateTime? To { get; init; }

	/// <inheritdoc/>
	public WpPostId? ParentId { get; init; }

	/// <inheritdoc/>
	public IImmutableList<(Taxonomy taxonomy, WpTermId id)> Taxonomies { get; init; } =
		new ImmutableList<(Taxonomy taxonomy, WpTermId id)>();

	/// <inheritdoc/>
	public IImmutableList<(ICustomField field, Compare cmp, object value)> CustomFields { get; init; } =
		new ImmutableList<(ICustomField field, Compare cmp, object value)>();

	/// <summary>
	/// Allow Builder to be injected
	/// </summary>
	/// <param name="schema">IWpDbSchema</param>
	/// <param name="builder">IQueryPostsPartsBuilder</param>
	protected PostsOptions(IWpDbSchema schema, IQueryPostsPartsBuilder builder) : base(schema, builder) { }
}
