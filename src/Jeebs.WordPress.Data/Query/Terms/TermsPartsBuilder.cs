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

namespace Jeebs.WordPress.Data
{
	public static partial class Query
	{
		/// <inheritdoc cref="IQueryPostsPartsBuilder"/>
		public sealed class TermsPartsBuilder : PartsBuilder<WpTermId>, IQueryTermsPartsBuilder
		{
			/// <inheritdoc/>
			public override ITable Table =>
				T.Term;

			/// <inheritdoc/>
			public override IColumn IdColumn =>
				new Column(T.Term.GetName(), T.Term.TermId, nameof(T.Term.TermId));

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			internal TermsPartsBuilder(IWpDbSchema schema) : base(schema) { }

			/// <inheritdoc/>
			public override IColumnList GetColumns<TModel>() =>
				Extract.From<TModel>(Table, T.TermTaxonomy);

			/// <inheritdoc/>
			public Option<QueryParts> AddWhereTaxonomy(QueryParts parts, Taxonomy? Taxonomy)
			{
				// Add Taxonomy
				if (Taxonomy is Taxonomy taxonomy)
				{
					return AddWhere(parts, T.TermTaxonomy, t => t.Taxonomy, Compare.Equal, taxonomy);
				}

				// Return
				return parts;
			}

			/// <inheritdoc/>
			public Option<QueryParts> AddWhereSlug(QueryParts parts, string? Slug)
			{
				// Add Slug
				if (!string.IsNullOrEmpty(Slug))
				{
					return AddWhere(parts, T.Term, t => t.Slug, Compare.Equal, Slug);
				}

				// Return
				return parts;
			}

			/// <inheritdoc/>
			public Option<QueryParts> AddWhereCount(QueryParts parts, long CountAtLeast)
			{
				// Add Count
				if (CountAtLeast > 0)
				{
					return AddWhere(parts, T.TermTaxonomy, t => t.Count, Compare.MoreThanOrEqual, CountAtLeast);
				}

				// Return
				return parts;
			}

			/// <inheritdoc/>
			public override Option<QueryParts> AddSort(QueryParts parts, bool sortRandom, IImmutableList<(IColumn, SortOrder)> sort) =>
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
