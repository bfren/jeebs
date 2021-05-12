// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using Jeebs.WordPress.Data.Tables;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsTaxonomyOptions"/>
		public sealed record PostsTaxonomyOptions : Options<WpTermId, TermTable>, IQueryPostsTaxonomyOptions
		{
			private new IQueryPostsTaxonomyPartsBuilder Builder =>
				(IQueryPostsTaxonomyPartsBuilder)base.Builder;

			/// <inheritdoc/>
			public IImmutableList<Taxonomy> Taxonomies { get; init; } =
				new ImmutableList<Taxonomy>();

			/// <inheritdoc/>
			public IImmutableList<WpPostId> PostIds { get; init; } =
				new ImmutableList<WpPostId>();

			/// <inheritdoc/>
			public TaxonomySort SortBy { get; init; }

			/// <inheritdoc/>
			protected override Expression<Func<TermTable, string>> IdColumn =>
				table => table.TermId;

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal PostsTaxonomyOptions(IWpDb db) : base(db, new PostsTaxonomyPartsBuilder(db.Schema), db.Schema.Term) =>
				Maximum = null;

			/// <inheritdoc/>
			protected override Option<IColumnList> GetColumns<TModel>(ITable table) =>
				Extract<TModel>.From(table, T.TermRelationship, T.TermTaxonomy);

			/// <inheritdoc/>
			protected override Option<QueryParts> BuildParts(ITable table, IColumnList cols, IColumn idColumn) =>
				Builder.Create(
					table, cols, Maximum, Skip
				)
				.Bind(
					x => Builder.AddInnerJoin(x, T.Term, t => t.TermId, T.TermTaxonomy, tx => tx.TermId)
				)
				.Bind(
					x => Builder.AddInnerJoin(x, T.TermTaxonomy, tx => tx.TermTaxonomyId, T.TermRelationship, tr => tr.TermTaxonomyId)
				)
				.SwitchIf(
					_ => Id?.Value > 0 || Ids.Count > 0,
					x => Builder.AddWhereId(x, idColumn, Id, Ids)
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
