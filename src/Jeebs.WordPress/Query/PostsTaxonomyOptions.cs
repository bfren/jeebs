// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using Maybe;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="Options.IQueryPostsTaxonomyOptions"/>
public sealed record class PostsTaxonomyOptions : Options.PostsTaxonomyOptions
{
	/// <summary>
	/// Internal creation only
	/// </summary>
	/// <param name="schema">IWpDbSchema</param>
	internal PostsTaxonomyOptions(IWpDbSchema schema) : base(schema, new PostsTaxonomyPartsBuilder(schema)) { }

	/// <summary>
	/// Allow Builder to be injected
	/// </summary>
	/// <param name="schema">IWpDbSchema</param>
	/// <param name="builder">IQueryPostsTaxonomyPartsBuilder</param>
	internal PostsTaxonomyOptions(IWpDbSchema schema, IQueryPostsTaxonomyPartsBuilder builder) : base(schema, builder) { }

	/// <inheritdoc/>
	protected override Maybe<QueryParts> Build(Maybe<QueryParts> parts) =>
		base.Build(
			parts
		)
		.Bind(
			x => Builder.AddInnerJoin(x, T.Terms, t => t.Id, T.TermTaxonomies, tx => tx.TermId)
		)
		.Bind(
			x => Builder.AddInnerJoin(x, T.TermTaxonomies, tx => tx.Id, T.TermRelationships, tr => tr.TermTaxonomyId)
		)
		.SwitchIf(
			_ => Id?.Value > 0 || Ids.Count > 0,
			x => Builder.AddWhereId(x, Id, Ids)
		)
		.SwitchIf(
			_ => Taxonomies.Count > 0,
			ifTrue: x => Builder.AddWhereTaxonomies(x, Taxonomies)
		)
		.SwitchIf(
			_ => PostIds.Count > 0,
			ifTrue: x => Builder.AddWherePostIds(x, PostIds)
		)
		.Bind(
			x => Builder.AddSort(x, SortRandom, Sort, SortBy)
		);
}
