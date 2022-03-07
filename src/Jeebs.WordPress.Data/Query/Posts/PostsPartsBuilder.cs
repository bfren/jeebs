// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data;

public static partial class Query
{
	/// <inheritdoc cref="IQueryPostsPartsBuilder"/>
	public sealed class PostsPartsBuilder : PartsBuilder<WpPostId>, IQueryPostsPartsBuilder
	{
		/// <inheritdoc/>
		public override ITable Table =>
			T.Post;

		/// <inheritdoc/>
		public override IColumn IdColumn =>
			new Column(T.Post, T.Post.Id, nameof(T.Post.Id));

		/// <summary>
		/// Internal creation only
		/// </summary>
		/// <param name="schema">IWpDbSchema</param>
		internal PostsPartsBuilder(IWpDbSchema schema) : base(schema) { }

		/// <summary>
		/// Internal creation only
		/// </summary>
		/// <param name="extract">IExtract</param>
		/// <param name="schema">IWpDbSchema</param>
		internal PostsPartsBuilder(IExtract extract, IWpDbSchema schema) : base(extract, schema) { }

		/// <inheritdoc/>
		public Maybe<QueryParts> AddWhereType(QueryParts parts, PostType type) =>
			AddWhere(parts, T.Post, p => p.Type, Compare.Equal, type);

		/// <inheritdoc/>
		public Maybe<QueryParts> AddWhereStatus(QueryParts parts, PostStatus status) =>
			AddWhere(parts, T.Post, p => p.Status, Compare.Equal, status);

		/// <inheritdoc/>
		public Maybe<QueryParts> AddWhereSearch(QueryParts parts, SearchPostField fields, Compare cmp, string? text)
		{
			// If there isn't any search text, don't do anything
			if (text is null)
			{
				return parts;
			}

			// Create clause
			var clause = new StringBuilder();

			// Trim search text
			var search = text.Trim();

			// Get comparison operator - if it's LIKE and % has not already been added to the search string, add it
			var comparison = cmp.ToOperator();
			if (cmp == Compare.Like && !search.Contains('%', StringComparison.CurrentCulture))
			{
				search = $"%{search}%";
			}

			// Search title
			if ((fields & SearchPostField.Title) != 0)
			{
				_ = clause.Append($"{__(T.Post, p => p.Title)} {comparison} @{nameof(search)}");
			}

			// Search slug
			if ((fields & SearchPostField.Slug) != 0)
			{
				if (clause.Length > 0)
				{
					_ = clause.Append(" OR ");
				}

				_ = clause.Append($"{__(T.Post, p => p.Slug)} {comparison} @{nameof(search)}");
			}

			// Search content
			if ((fields & SearchPostField.Content) != 0)
			{
				if (clause.Length > 0)
				{
					_ = clause.Append(" OR ");
				}

				_ = clause.Append($"{__(T.Post, p => p.Content)} {comparison} @{nameof(search)}");
			}

			// Search excerpt
			if ((fields & SearchPostField.Excerpt) != 0)
			{
				if (clause.Length > 0)
				{
					_ = clause.Append(" OR ");
				}

				_ = clause.Append($"{__(T.Post, p => p.Excerpt)} {comparison} @{nameof(search)}");
			}

			// Return
			return AddWhereCustom(parts, clause.ToString(), new { search });
		}

		/// <inheritdoc/>
		public Maybe<QueryParts> AddWherePublishedFrom(QueryParts parts, DateTime? fromDate)
		{
			// Add From (use start of the day)
			if (fromDate is DateTime fromBase)
			{
				var start = fromBase.StartOfDay().ToMySqlString();
				return AddWhere(parts, T.Post, p => p.PublishedOn, Compare.MoreThanOrEqual, start);
			}

			// Return
			return parts;
		}

		/// <inheritdoc/>
		public Maybe<QueryParts> AddWherePublishedTo(QueryParts parts, DateTime? toDate)
		{
			// Add To (use end of the day)
			if (toDate is DateTime toBase)
			{
				var end = toBase.EndOfDay().ToMySqlString();
				return AddWhere(parts, T.Post, p => p.PublishedOn, Compare.LessThanOrEqual, end);
			}

			// Return
			return parts;
		}

		/// <inheritdoc/>
		public Maybe<QueryParts> AddWhereParentId(QueryParts parts, WpPostId? parentId)
		{
			// Add Parent ID
			if (parentId?.Value > 0)
			{
				return AddWhere(parts, T.Post, p => p.ParentId, Compare.Equal, parentId.Value);
			}

			// Return
			return parts;
		}

		/// <inheritdoc/>
		public Maybe<QueryParts> AddWhereTaxonomies(QueryParts parts, IImmutableList<(Taxonomy taxonomy, WpTermId id)> taxonomies)
		{
			// If there aren't any, don't do anything
			if (taxonomies.Count == 0)
			{
				return parts;
			}

			// Setup variables
			var taxonomyWhere = new StringBuilder();
			var taxonomyNameIndex = 0;
			var taxonomyParameters = new QueryParameters();

			// Group taxonomies by taxonomy name
			var grouped = from t in taxonomies
						  group t by t.taxonomy into g
						  select new
						  {
							  Name = g.Key,
							  Ids = g.Select(x => x.id.Value).ToList()
						  };

			// Add each taxonomy
			foreach (var taxonomy in grouped)
			{
				// Add AND if this is not the first conditional clause
				if (taxonomyWhere.Length > 0)
				{
					_ = taxonomyWhere.Append(" AND ");
				}

				// Name of the taxonomy parameter
				var taxonomyNameParameter = $"@taxonomy{taxonomyNameIndex}";
				taxonomyParameters.Add(taxonomyNameParameter, taxonomy.Name);

				// Add SQL commands to lookup taxonomy terms
				var subQuery = new StringBuilder(
					"SELECT COUNT(1) " +
					$"FROM {__(T.TermRelationship)} " +
					$"INNER JOIN {__(T.TermTaxonomy)} ON {__(T.TermRelationship, tr => tr.TermTaxonomyId)} = {__(T.TermTaxonomy, tx => tx.Id)} " +
					$"WHERE {__(T.TermTaxonomy, tx => tx.Taxonomy)} = {taxonomyNameParameter} " +
					$"AND {__(T.TermRelationship, tr => tr.PostId)} = {__(T.Post, p => p.Id)} " +
					$"AND {__(T.TermTaxonomy, tx => tx.TermId)} IN ("
				);

				// Add the terms for this taxonomy
				var taxonomyIdIndex = 0;
				foreach (var taxonomyId in taxonomy.Ids)
				{
					// Add a comma if this is not the first term
					if (taxonomyIdIndex > 0)
					{
						_ = subQuery.Append(", ");
					}

					// Add the term parameter and reference
					var taxonomyIdParameter = $"{taxonomyNameParameter}_{taxonomyIdIndex}";

					_ = subQuery.Append(taxonomyIdParameter);
					taxonomyParameters.Add(taxonomyIdParameter, taxonomyId);

					// Increase taxonomy term index
					taxonomyIdIndex++;
				}

				// Close IN function
				_ = subQuery.Append(')');

				// Add to sub-query, matching the number of terms
				_ = taxonomyWhere.Append($"({subQuery}) = {taxonomy.Ids.Count}");

				// Increase taxonomy name index
				taxonomyNameIndex++;
			}

			// Add to main WHERE clause
			return AddWhereCustom(parts, taxonomyWhere.ToString(), taxonomyParameters);
		}

		/// <inheritdoc/>
		public Maybe<QueryParts> AddWhereCustomFields(QueryParts parts, IImmutableList<(ICustomField, Compare, object)> customFields)
		{
			// If there aren't any, don't do anything
			if (customFields.Count == 0)
			{
				return parts;
			}

			// Setup variables
			var customFieldWhere = new StringBuilder();
			var customFieldIndex = 0;
			var customFieldParameters = new QueryParameters();

			// Add each custom field
			foreach (var (field, cmp, value) in customFields)
			{
				// Add AND if this is not the first conditional clause
				if (customFieldWhere.Length > 0)
				{
					_ = customFieldWhere.Append(" AND ");
				}

				// Ensure there is a search value
				var customFieldSearch = value.ToString();
				if (customFieldSearch is null)
				{
					continue;
				}

				// Get comparison operator - if it's LIKE and % has not already been added to the search string, add it
				var customFieldComparison = cmp.ToOperator();
				if (cmp == Compare.Like && !customFieldSearch.Contains('%', StringComparison.CurrentCulture))
				{
					customFieldSearch = $"%{customFieldSearch}%";
				}

				// Name of the custom field parameter
				var customFieldKeyParameter = $"@customField{customFieldIndex}_Key";
				var customFieldValueParameter = $"@customField{customFieldIndex}_Value";
				customFieldParameters.Add(customFieldKeyParameter, field.Key);
				customFieldParameters.Add(customFieldValueParameter, customFieldSearch);

				// Add SQL commands to lookup custom field
				var subQuery = new StringBuilder(
					"SELECT COUNT(1) " +
					$"FROM {__(T.PostMeta)} " +
					$"WHERE {__(T.PostMeta, pm => pm.PostId)} = {__(T.Post, p => p.Id)} " +
					$"AND {__(T.PostMeta, pm => pm.Key)} = {customFieldKeyParameter} " +
					$"AND {__(T.PostMeta, pm => pm.Value)} {customFieldComparison} {customFieldValueParameter}"
				);

				// Add sub query to where
				_ = customFieldWhere.Append($"({subQuery}) = 1");

				// Increase custom field index
				customFieldIndex++;
			}

			// Add to main WHERE clause
			return AddWhereCustom(parts, customFieldWhere.ToString(), customFieldParameters);
		}
	}
}
