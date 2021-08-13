// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Querying;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsTaxonomyOptions"/>
		public sealed record class PostsTaxonomyOptions : Querying.PostsTaxonomyOptions
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
			protected override Option<QueryParts> Build(Option<QueryParts> parts) =>
				base.Build(
					parts
				)
				.Bind(
					x => Builder.AddInnerJoin(x, T.Term, t => t.Id, T.TermTaxonomy, tx => tx.TermId)
				)
				.Bind(
					x => Builder.AddInnerJoin(x, T.TermTaxonomy, tx => tx.Id, T.TermRelationship, tr => tr.TermTaxonomyId)
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
	}
}
