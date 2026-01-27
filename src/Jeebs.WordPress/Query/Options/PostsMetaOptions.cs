// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.WordPress.Entities.Ids;

namespace Jeebs.WordPress.Query.Options;

/// <inheritdoc cref="IQueryPostsMetaOptions"/>
public abstract record class PostsMetaOptions : Options<WpPostMetaId>, IQueryPostsMetaOptions
{
	/// <summary>
	/// IQueryPostsMetaPartsBuilder.
	/// </summary>
	protected new IQueryPostsMetaPartsBuilder Builder =>
		(IQueryPostsMetaPartsBuilder)base.Builder;

	/// <inheritdoc/>
	public WpPostId? PostId { get; init; }

	/// <inheritdoc/>
	public IImmutableList<WpPostId> PostIds { get; init; } =
		new ImmutableList<WpPostId>();

	/// <inheritdoc/>
	public string? Key { get; init; }

	/// <summary>
	/// Allow Builder to be injected.
	/// </summary>
	/// <param name="schema">IWpDbSchema.</param>
	/// <param name="builder">IQueryPostsMetaPartsBuilder.</param>
	protected PostsMetaOptions(IWpDbSchema schema, IQueryPostsMetaPartsBuilder builder) : base(schema, builder) =>
		Maximum = null;
}
