// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Linq;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.Linq;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using static F.DataF.QueryF;
using static F.OptionF;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsTaxonomyOptions{TEntity}{TEntity}"/>
		public sealed record PostsTaxonomy<TEntity> : Options<TEntity, WpTermId>, IQueryPostsTaxonomyOptions<TEntity>
			where TEntity : WpTermEntity
		{
			/// <inheritdoc/>
			public IImmutableList<Taxonomy>? Taxonomies { get; init; }

			/// <inheritdoc/>
			public IImmutableList<long>? PostIds { get; init; }

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal PostsTaxonomy(IWpDb db) : base(db) { }

			/// <inheritdoc/>
			public override Option<IQueryParts> GetParts<TModel>() =>
				from cols in Extract<TModel>.From(T.Term, T.TermRelationship, T.TermTaxonomy)
				from parts in GetParts(cols)
				select (IQueryParts)parts;

			/// <summary>
			/// Actually get the various query parts using helper functions
			/// </summary>
			/// <param name="cols">Select ColumnList</param>
			internal Option<QueryParts> GetParts(IColumnList cols) =>
				Return(
					new QueryParts(T.Term)
					{
						Maximum = null,
						Skip = 0
					}
				)
				.Bind(
					x => AddInnerJoin(x, (T.Term, t => t.TermId), (T.TermTaxonomy, tx => tx.TermId))
				)
				.Bind(
					x => AddInnerJoin(x, (T.TermTaxonomy, tx => tx.TermTaxonomyId), (T.TermRelationship, tr => tr.TermTaxonomyId))
				)
				.Bind(
					x => AddSelect(x, cols)
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
					Sort = parts.Sort.WithRange((title, SortOrder.Ascending), (count, SortOrder.Descending))
				};
		}
	}
}
