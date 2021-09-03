// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.Linq;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using static F.DataF.QueryF;

namespace Jeebs.WordPress.Data;

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
			new Column(T.Term, T.Term.Id, nameof(T.Term.Id));

		/// <summary>
		/// Internal creation only
		/// </summary>
		/// <param name="schema">IWpDbSchema</param>
		internal TermsPartsBuilder(IWpDbSchema schema) : base(schema) { }

		/// <summary>
		/// Internal creation only
		/// </summary>
		/// <param name="extract">IExtract</param>
		/// <param name="schema">IWpDbSchema</param>
		internal TermsPartsBuilder(IExtract extract, IWpDbSchema schema) : base(extract, schema) { }

		/// <inheritdoc/>
		public override IColumnList GetColumns<TModel>() =>
			Extract.From<TModel>(Table, T.TermTaxonomy);

		/// <inheritdoc/>
		public Option<QueryParts> AddWhereTaxonomy(QueryParts parts, Taxonomy? taxonomy)
		{
			// Add Taxonomy
			if (taxonomy is not null)
			{
				return AddWhere(parts, T.TermTaxonomy, t => t.Taxonomy, Compare.Equal, taxonomy);
			}

			// Return
			return parts;
		}

		/// <inheritdoc/>
		public Option<QueryParts> AddWhereSlug(QueryParts parts, string? slug)
		{
			// Add Slug
			if (!string.IsNullOrEmpty(slug))
			{
				return AddWhere(parts, T.Term, t => t.Slug, Compare.Equal, slug);
			}

			// Return
			return parts;
		}

		/// <inheritdoc/>
		public Option<QueryParts> AddWhereCount(QueryParts parts, long countAtLeast)
		{
			// Add Count
			if (countAtLeast > 0)
			{
				return AddWhere(parts, T.TermTaxonomy, t => t.Count, Compare.MoreThanOrEqual, countAtLeast);
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
