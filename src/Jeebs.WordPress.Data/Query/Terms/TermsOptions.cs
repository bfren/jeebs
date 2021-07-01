// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryTermsOptions"/>
		public sealed record TermsOptions : Options<WpTermId>, IQueryTermsOptions
		{
			private new IQueryTermsPartsBuilder Builder =>
				(IQueryTermsPartsBuilder)base.Builder;

			/// <inheritdoc/>
			public Taxonomy? Taxonomy { get; init; }

			/// <inheritdoc/>
			public string? Slug { get; init; }

			/// <inheritdoc/>
			public long CountAtLeast { get; init; } = 1;

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			internal TermsOptions(IWpDbSchema schema) : base(schema, new TermsPartsBuilder(schema)) =>
				Maximum = null;

			/// <summary>
			/// Allow Builder to be injected
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			/// <param name="builder">IQueryTermsPartsBuilder</param>
			internal TermsOptions(IWpDbSchema schema, IQueryTermsPartsBuilder builder) : base(schema, builder) { }

			/// <inheritdoc/>
			protected override Option<QueryParts> Build(Option<QueryParts> parts) =>
				base.Build(
					parts
				)
				.Bind(
					x => Builder.AddInnerJoin(x, T.Term, t => t.TermId, T.TermTaxonomy, tx => tx.TermId)
				)
				.SwitchIf(
					_ => Taxonomy is not null,
					ifTrue: x => Builder.AddWhereTaxonomy(x, Taxonomy)
				)
				.SwitchIf(
					_ => string.IsNullOrEmpty(Slug),
					ifFalse: x => Builder.AddWhereSlug(x, Slug)
				)
				.SwitchIf(
					_ => CountAtLeast > 0,
					ifTrue: x => Builder.AddWhereCount(x, CountAtLeast)
				);
		}
	}
}
