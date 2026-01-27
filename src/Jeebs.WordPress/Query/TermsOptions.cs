// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="Options.IQueryTermsOptions"/>
public sealed record class TermsOptions : Options.TermsOptions
{
	/// <summary>
	/// Internal creation only.
	/// </summary>
	/// <param name="schema">IWpDbSchema.</param>
	internal TermsOptions(IWpDbSchema schema) : base(schema, new TermsPartsBuilder(schema)) { }

	/// <summary>
	/// Allow Builder to be injected.
	/// </summary>
	/// <param name="schema">IWpDbSchema.</param>
	/// <param name="builder">IQueryTermsPartsBuilder.</param>
	internal TermsOptions(IWpDbSchema schema, IQueryTermsPartsBuilder builder) : base(schema, builder) { }

	/// <inheritdoc/>
	protected override Result<QueryParts> Build(Result<QueryParts> parts) =>
		base.Build(
			parts
		)
		.Bind(
			x => Builder.AddInnerJoin(x, T.Terms, t => t.Id, T.TermTaxonomies, tx => tx.TermId)
		)
		.If(
			_ => Taxonomy is not null,
			x => Builder.AddWhereTaxonomy(x, Taxonomy)
		)
		.If(
			_ => !string.IsNullOrEmpty(Slug),
			x => Builder.AddWhereSlug(x, Slug)
		)
		.If(
			_ => CountAtLeast > 0,
			x => Builder.AddWhereCount(x, CountAtLeast)
		);
}
