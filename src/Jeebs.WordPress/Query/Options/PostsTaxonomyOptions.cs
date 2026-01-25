// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.Query.Options;

/// <inheritdoc cref="IQueryPostsTaxonomyOptions"/>
public abstract record class PostsTaxonomyOptions : Options<WpTermId>, IQueryPostsTaxonomyOptions
{
	/// <summary>
	/// IQueryPostsTaxonomyPartsBuilder.
	/// </summary>
	protected new IQueryPostsTaxonomyPartsBuilder Builder =>
		(IQueryPostsTaxonomyPartsBuilder)base.Builder;

	/// <inheritdoc/>
	public IImmutableList<Taxonomy> Taxonomies { get; init; } =
		new ImmutableList<Taxonomy>();

	/// <inheritdoc/>
	public IImmutableList<WpPostId> PostIds { get; init; } =
		new ImmutableList<WpPostId>();

	/// <inheritdoc/>
	public TaxonomySort SortBy { get; init; }

	/// <summary>
	/// Allow Builder to be injected.
	/// </summary>
	/// <param name="schema">IWpDbSchema.</param>
	/// <param name="builder">IQueryPostsTaxonomyPartsBuilder.</param>
	protected PostsTaxonomyOptions(IWpDbSchema schema, IQueryPostsTaxonomyPartsBuilder builder) : base(schema, builder) =>
		Maximum = null;
}
