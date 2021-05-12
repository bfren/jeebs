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
		/// <inheritdoc cref="IQueryTermsOptions"/>
		public sealed record TermsOptions : Options<WpTermId, TermTable>, IQueryTermsOptions
		{
			private new IQueryTermsPartsBuilder Builder =>
				(IQueryTermsPartsBuilder)base.Builder;

			/// <inheritdoc/>
			public Taxonomy? Taxonomy { get; init; }

			/// <inheritdoc/>
			public string? Slug { get; init; }

			/// <inheritdoc/>
			public long CountAtLeast { get; init; } = 1;

			/// <inheritdoc/>
			protected override Expression<Func<TermTable, string>> IdColumn =>
				table => table.TermId;

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal TermsOptions(IWpDb db) : base(db, new TermsPartsBuilder(db.Schema), db.Schema.Term) =>
				Maximum = null;

			/// <inheritdoc/>
			protected override Option<IColumnList> GetColumns<TModel>(ITable table) =>
				Extract<TModel>.From(table, T.TermTaxonomy);

			/// <inheritdoc/>
			protected override Option<QueryParts> BuildParts(ITable table, IColumnList cols, IColumn idColumn) =>
				Builder.Create(
					table, cols, Maximum, Skip
				)
				.Bind(
					x => Builder.AddInnerJoin(x, T.Term, t => t.TermId, T.TermTaxonomy, tx => tx.TermId)
				)
				.SwitchIf(
					_ => Id is not null || Ids is not null,
					x => Builder.AddWhereId(x, idColumn, Id, Ids)
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
				)
				.Bind(
					x => Builder.AddSort(x, SortRandom, Sort)
				);
		}
	}
}
