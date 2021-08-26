// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.Linq;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using static F.DataF.QueryF;

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsPartsBuilder"/>
		public sealed class PostsTaxonomyPartsBuilder : PartsBuilder<WpTermId>, IQueryPostsTaxonomyPartsBuilder
		{
			/// <inheritdoc/>
			public override ITable Table =>
				T.Term;

			/// <inheritdoc/>
			public override IColumn IdColumn =>
				new Column(T.Term.GetName(), T.Term.Id, nameof(T.Term.Id));

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			internal PostsTaxonomyPartsBuilder(IWpDbSchema schema) : base(schema) { }

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="extract">IExtract</param>
			/// <param name="schema">IWpDbSchema</param>
			internal PostsTaxonomyPartsBuilder(IExtract extract, IWpDbSchema schema) : base(extract, schema) { }

			/// <inheritdoc/>
			public override IColumnList GetColumns<TModel>() =>
				Extract.From<TModel>(Table, T.TermRelationship, T.TermTaxonomy);

			/// <inheritdoc/>
			public Option<QueryParts> AddWhereTaxonomies(QueryParts parts, IImmutableList<Taxonomy> taxonomies)
			{
				// Add Taxonomies
				if (taxonomies.Count > 0)
				{
					return AddWhere(parts, T.TermTaxonomy, t => t.Taxonomy, Compare.In, taxonomies);
				}

				// Return
				return parts;
			}

			/// <inheritdoc/>
			public Option<QueryParts> AddWherePostIds(QueryParts parts, IImmutableList<WpPostId> postIds)
			{
				// Add Post IDs
				if (postIds.Count > 0)
				{
					var postIdValues = postIds.Select(p => p.Value);
					return AddWhere(parts, T.TermRelationship, t => t.PostId, Compare.In, postIdValues);
				}

				// Return
				return parts;
			}

			/// <inheritdoc/>
			public Option<QueryParts> AddSort(
				QueryParts parts,
				bool sortRandom,
				IImmutableList<(IColumn, SortOrder)> sort,
				TaxonomySort sortBy
			)
			{
				// Add base (custom) Sort options
				if (sortRandom || sort.Count > 0)
				{
					return AddSort(parts, sortRandom, sort);
				}

				// Add default sort
				return from title in GetColumnFromExpression(T.Term, t => t.Title)
					   from count in GetColumnFromExpression(T.TermTaxonomy, tx => tx.Count)
					   let sortRange = sortBy switch
					   {
						   TaxonomySort.CountDescending =>
							   new[] { (count, SortOrder.Descending), (title, SortOrder.Ascending) },

						   _ =>
							   new[] { (title, SortOrder.Ascending) }
					   }
					   select parts with { Sort = parts.Sort.WithRange(sortRange) };
			}
		}
	}
}
