// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.Data.Query.Functions;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;
using MaybeF;
using MaybeF.Linq;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="IQueryPostsPartsBuilder"/>
public sealed class TermsPartsBuilder : PartsBuilder<WpTermId>, IQueryTermsPartsBuilder
{
	/// <inheritdoc/>
	public override ITable Table =>
		T.Terms;

	/// <inheritdoc/>
	public override IColumn IdColumn =>
		new Column(T.Terms, T.Terms.Id, nameof(T.Terms.Id));

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
		Extract.From<TModel>(Table, T.TermTaxonomies);

	/// <inheritdoc/>
	public Maybe<QueryParts> AddWhereTaxonomy(QueryParts parts, Taxonomy? taxonomy)
	{
		// Add Taxonomy
		if (taxonomy is not null)
		{
			return AddWhere(parts, T.TermTaxonomies, t => t.Taxonomy, Compare.Equal, taxonomy);
		}

		// Return
		return parts;
	}

	/// <inheritdoc/>
	public Maybe<QueryParts> AddWhereSlug(QueryParts parts, string? slug)
	{
		// Add Slug
		if (!string.IsNullOrEmpty(slug))
		{
			return AddWhere(parts, T.Terms, t => t.Slug, Compare.Equal, slug);
		}

		// Return
		return parts;
	}

	/// <inheritdoc/>
	public Maybe<QueryParts> AddWhereCount(QueryParts parts, long countAtLeast)
	{
		// Add Count
		if (countAtLeast > 0)
		{
			return AddWhere(parts, T.TermTaxonomies, t => t.Count, Compare.MoreThanOrEqual, countAtLeast);
		}

		// Return
		return parts;
	}

	/// <inheritdoc/>
	public override Maybe<QueryParts> AddSort(QueryParts parts, bool sortRandom, IImmutableList<(IColumn, SortOrder)> sort) =>
		from title in QueryF.GetColumnFromExpression(T.Terms, t => t.Title)
		from count in QueryF.GetColumnFromExpression(T.TermTaxonomies, tx => tx.Count)
		select parts with
		{
			Sort = parts.Sort.WithRange(
				(title, SortOrder.Ascending),
				(count, SortOrder.Descending)
			)
		};
}
