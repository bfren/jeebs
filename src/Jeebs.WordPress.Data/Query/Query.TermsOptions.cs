// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.Linq;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using Jeebs.WordPress.Data.Tables;
using static F.DataF.QueryF;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryTermsOptions"/>
		public sealed record TermsOptions : Options<WpTermId, TermTable>, IQueryTermsOptions
		{
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
			internal TermsOptions(IWpDb db) : base(db, db.Schema.Term) =>
				Maximum = null;

			/// <inheritdoc/>
			protected override Option<IColumnList> GetColumns<TModel>(ITable table) =>
				Extract<TModel>.From(table, T.TermTaxonomy);

			/// <inheritdoc/>
			protected override Option<QueryParts> GetParts(ITable table, IColumnList cols, IColumn idColumn) =>
				CreateParts(
					table, cols
				)
				.Bind(
					x => AddInnerJoin(x, (T.Term, t => t.TermId), (T.TermTaxonomy, tx => tx.TermId))
				)
				.SwitchIf(
					_ => Id is not null || Ids is not null,
					x => AddWhereId(x, idColumn)
				)
				.SwitchIf(
					_ => Taxonomy is not null,
					ifTrue: AddWhereTaxonomy
				)
				.SwitchIf(
					_ => string.IsNullOrEmpty(Slug),
					ifFalse: AddWhereSlug
				)
				.SwitchIf(
					_ => CountAtLeast > 0,
					ifTrue: AddWhereCount
				)
				.Bind(
					x => AddSort(x)
				);

			/// <summary>
			/// Add Where Taxonomy
			/// </summary>
			/// <param name="parts">QueryParts</param>
			internal Option<QueryParts> AddWhereTaxonomy(QueryParts parts)
			{
				// Add Taxonomy
				if (Taxonomy is Taxonomy taxonomy)
				{
					return AddWhere(parts, T.TermTaxonomy, t => t.Taxonomy, Compare.Equal, taxonomy);
				}

				// Return
				return parts;
			}

			/// <summary>
			/// Add Where Slug
			/// </summary>
			/// <param name="parts">QueryParts</param>
			internal Option<QueryParts> AddWhereSlug(QueryParts parts)
			{
				// Add Slug
				if (Slug is string slug)
				{
					return AddWhere(parts, T.Term, t => t.Slug, Compare.Equal, slug);
				}

				// Return
				return parts;
			}

			/// <summary>
			/// Add Where Count
			/// </summary>
			/// <param name="parts">QueryParts</param>
			internal Option<QueryParts> AddWhereCount(QueryParts parts)
			{
				// Add Count
				if (CountAtLeast is long count)
				{
					return AddWhere(parts, T.TermTaxonomy, t => t.Count, Compare.MoreThanOrEqual, count);
				}

				// Return
				return parts;
			}

			/// <summary>
			/// Sort by Title and then Count
			/// </summary>
			/// <param name="parts">QueryParts</param>
			protected override Option<QueryParts> AddSort(QueryParts parts) =>
				from title in GetColumnFromExpression(T.Term, t => t.Title)
				from count in GetColumnFromExpression(T.TermTaxonomy, tx => tx.Count)
				select parts with
				{
					Sort = parts.Sort.WithRange(
						(title, SortOrder.Ascending),
						(count, SortOrder.Descending)
					)
				};
		}
	}
}
