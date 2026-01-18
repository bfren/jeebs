// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.Query.Options;

/// <inheritdoc cref="IQueryTermsOptions"/>
public abstract record class TermsOptions : Options<WpTermId>, IQueryTermsOptions
{
	/// <summary>
	/// IQueryTermsPartsBuilder.
	/// </summary>
	protected new IQueryTermsPartsBuilder Builder =>
		(IQueryTermsPartsBuilder)base.Builder;

	/// <inheritdoc/>
	public Taxonomy? Taxonomy { get; init; }

	/// <inheritdoc/>
	public string? Slug { get; init; }

	/// <inheritdoc/>
	public long CountAtLeast { get; init; } = 1;

	/// <summary>
	/// Allow Builder to be injected.
	/// </summary>
	/// <param name="schema">IWpDbSchema.</param>
	/// <param name="builder">IQueryTermsPartsBuilder.</param>
	protected TermsOptions(IWpDbSchema schema, IQueryTermsPartsBuilder builder) : base(schema, builder) =>
		Maximum = null;
}
