// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
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
		/// <inheritdoc cref="IQueryPostsTaxonomyOptions"/>
		public sealed record PostsTaxonomyOptions : Options<WpTermId, TermTable>, IQueryPostsTaxonomyOptions
		{
			/// <inheritdoc/>
			public IImmutableList<Taxonomy>? Taxonomies { get; init; }

			/// <inheritdoc/>
			public IImmutableList<WpPostId>? PostIds { get; init; }

			/// <inheritdoc/>
			protected override Expression<Func<TermTable, string>> IdColumn =>
				table => table.TermId;

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal PostsTaxonomyOptions(IWpDb db) : base(db, db.Schema.Term) =>
				Maximum = null;

			/// <inheritdoc/>
			protected override Option<IColumnList> GetColumns<TModel>(ITable table) =>
				Extract<TModel>.From(table, T.TermRelationship, T.TermTaxonomy);

			/// <inheritdoc/>
			protected override Option<QueryParts> GetParts(ITable table, IColumnList cols, IColumn idColumn) =>
				CreateParts(
					table, cols
				)
				.Bind(
					x => AddInnerJoin(x, T.Term, t => t.TermId, T.TermTaxonomy, tx => tx.TermId)
				)
				.Bind(
					x => AddInnerJoin(x, T.TermTaxonomy, tx => tx.TermTaxonomyId, T.TermRelationship, tr => tr.TermTaxonomyId)
				)
				.SwitchIf(
					_ => Id is not null || Ids is not null,
					x => AddWhereId(x, idColumn)
				)
				.SwitchIf(
					_ => Taxonomies?.Count > 0,
					ifTrue: AddWhereTaxonomies
				)
				.SwitchIf(
					_ => PostIds?.Count > 0,
					ifTrue: AddWherePostIds
				)
				.Bind(
					x => AddSort(x)
				);

			/// <summary>
			/// Add Where Taxonomies
			/// </summary>
			/// <param name="parts">QueryParts</param>
			internal Option<QueryParts> AddWhereTaxonomies(QueryParts parts)
			{
				// Add Taxonomies
				if (Taxonomies is ImmutableList<Taxonomy> taxonomies && taxonomies.Count > 0)
				{
					return AddWhere(parts, T.TermTaxonomy, t => t.Taxonomy, Compare.In, taxonomies);
				}

				// Return
				return parts;
			}

			/// <summary>
			/// Add Where Post IDs
			/// </summary>
			/// <param name="parts">QueryParts</param>
			internal Option<QueryParts> AddWherePostIds(QueryParts parts)
			{
				// Add Post IDs
				if (PostIds is ImmutableList<long> postIds && postIds.Count > 0)
				{
					return AddWhere(parts, T.TermRelationship, t => t.PostId, Compare.In, postIds);
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
