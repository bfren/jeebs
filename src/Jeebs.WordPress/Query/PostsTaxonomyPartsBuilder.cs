// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.Data.Query.Functions;
using Jeebs.WordPress.Entities.StrongIds;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.Query;

/// <inheritdoc cref="IQueryPostsPartsBuilder"/>
public sealed class PostsTaxonomyPartsBuilder : PartsBuilder<WpTermId>, IQueryPostsTaxonomyPartsBuilder
{
	/// <inheritdoc/>
	public override ITable Table =>
		T.Terms;

	/// <inheritdoc/>
	public override IColumn IdColumn =>
		new Column(T.Terms, T.Terms.Id, GetIdColumn(T.Terms));

	/// <summary>
	/// Internal creation only.
	/// </summary>
	/// <param name="schema">IWpDbSchema</param>
	internal PostsTaxonomyPartsBuilder(IWpDbSchema schema) : base(schema) { }

	/// <summary>
	/// Internal creation only.
	/// </summary>
	/// <param name="extract">IExtract</param>
	/// <param name="schema">IWpDbSchema</param>
	internal PostsTaxonomyPartsBuilder(IExtract extract, IWpDbSchema schema) : base(extract, schema) { }

	/// <inheritdoc/>
	public override IColumnList GetColumns<TModel>() =>
		Extract.From<TModel>(Table, T.TermRelationships, T.TermTaxonomies);

	/// <inheritdoc/>
	public Maybe<QueryParts> AddWhereTaxonomies(QueryParts parts, IImmutableList<Taxonomy> taxonomies)
	{
		// Add Taxonomies
		if (taxonomies.Count > 0)
		{
			return AddWhere(parts, T.TermTaxonomies, t => t.Taxonomy, Compare.In, taxonomies);
		}

		// Return
		return parts;
	}

	/// <inheritdoc/>
	public Maybe<QueryParts> AddWherePostIds(QueryParts parts, IImmutableList<WpPostId> postIds)
	{
		// Add Post IDs
		if (postIds.Count > 0)
		{
			var postIdValues = postIds.Select(p => p.Value);
			return AddWhere(parts, T.TermRelationships, t => t.PostId, Compare.In, postIdValues);
		}

		// Return
		return parts;
	}

	/// <inheritdoc/>
	public Maybe<QueryParts> AddSort(
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
		return from title in QueryF.GetColumnFromExpression(T.Terms, t => t.Title)
			   from count in QueryF.GetColumnFromExpression(T.TermTaxonomies, tx => tx.Count)
			   let sortRange = sortBy switch
			   {
				   TaxonomySort.CountDescending =>
					   new[] { (count, SortOrder.Descending), (title, SortOrder.Ascending) },

				   _ =>
					   [(title, SortOrder.Ascending)]
			   }
			   select parts with { Sort = parts.Sort.WithRange(sortRange) };
	}
}
