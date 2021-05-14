// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data
{
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
				new Column(T.Post.GetName(), T.Post.PostId, nameof(T.Post.PostId));

			/// <summary>
			/// Internal creation only
			/// </summary>
			/// <param name="schema">IWpDbSchema</param>
			internal PostsPartsBuilder(IWpDbSchema schema) : base(schema) { }

			/// <inheritdoc/>
			public Option<QueryParts> AddWhereType(QueryParts parts, PostType type) =>
				AddWhere(parts, T.Post, p => p.Type, Compare.Equal, type);

			/// <inheritdoc/>
			public Option<QueryParts> AddWhereStatus(QueryParts parts, PostStatus status) =>
				AddWhere(parts, T.Post, p => p.Status, Compare.Equal, status);

			/// <inheritdoc/>
			public Option<QueryParts> AddWhereSearch(QueryParts parts, string? text, SearchPostFields fields, Compare cmp)
			{
				// If there isn't any search text, don't do anything
				if (text is null)
				{
					return parts;
				}

				// Create objects
				var clause = new StringBuilder();

				// Trim search text
				var search = text.Trim();

				// Set comparison operator and modify search string accordingly
				var comparison = "=";
				if (cmp == Compare.Like)
				{
					// Change the comparison
					comparison = "LIKE";

					// If % has not already been added to the search string, add it
					if (!search.Contains("%", StringComparison.CurrentCulture))
					{
						search = $"%{search}%";
					}
				}

				// Search title
				if ((fields & SearchPostFields.Title) != 0)
				{
					clause.Append($"{__(T.Post, p => p.Title)} {comparison} @{nameof(search)}");
				}

				// Search slug
				if ((fields & SearchPostFields.Slug) != 0)
				{
					if (clause.Length > 0)
					{
						clause.Append(" OR ");
					}

					clause.Append($"{__(T.Post, p => p.Slug)} {comparison} @{nameof(search)}");
				}

				// Search content
				if ((fields & SearchPostFields.Content) != 0)
				{
					if (clause.Length > 0)
					{
						clause.Append(" OR ");
					}

					clause.Append($"{__(T.Post, p => p.Content)} {comparison} @{nameof(search)}");
				}

				// Search excerpt
				if ((fields & SearchPostFields.Excerpt) != 0)
				{
					if (clause.Length > 0)
					{
						clause.Append(" OR ");
					}

					clause.Append($"{__(T.Post, p => p.Excerpt)} {comparison} @{nameof(search)}");
				}

				// Return
				return AddWhereCustom(parts, clause.ToString(), new { search });
			}

			/// <inheritdoc/>
			public Option<QueryParts> AddWherePublishedFrom(QueryParts parts, DateTime? from)
			{
				// Add From (use start of the day)
				if (from is DateTime fromBase)
				{
					var start = fromBase.StartOfDay().ToMySqlString();
					return AddWhere(parts, T.Post, p => p.PublishedOn, Compare.MoreThanOrEqual, start);
				}

				// Return
				return parts;
			}

			/// <inheritdoc/>
			public Option<QueryParts> AddWherePublishedTo(QueryParts parts, DateTime? to)
			{
				// Add To (use end of the day)
				if (to is DateTime toBase)
				{
					var end = toBase.EndOfDay().ToMySqlString();
					return AddWhere(parts, T.Post, p => p.PublishedOn, Compare.LessThanOrEqual, end);
				}

				// Return
				return parts;
			}

			/// <inheritdoc/>
			public Option<QueryParts> AddWhereParentId(QueryParts parts, long? parentId)
			{
				// Add Parent ID
				if (parentId > 0)
				{
					return AddWhere(parts, T.Post, p => p.ParentId, Compare.Equal, parentId);
				}

				// Return
				return parts;
			}

			/// <inheritdoc/>
			public Option<QueryParts> AddWhereTaxonomies(QueryParts parts, IImmutableList<(Taxonomy taxonomy, long id)> taxonomies)
			{
				// If there aren't any, don't do anything
				if (taxonomies.Count == 0)
				{
					return parts;
				}

				// Setup variables
				var taxonomyWhere = string.Empty;
				var taxonomyNameIndex = 0;
				var taxonomyParameters = new QueryParameters();

				// Group taxonomies by taxonomy name
				var grouped = from t in taxonomies
							  group t by t.taxonomy into g
							  select new
							  {
								  Name = g.Key,
								  Ids = g.Select(x => x.id).ToList()
							  };

				// Add each taxonomy
				foreach (var taxonomy in grouped)
				{
					// Add AND if this is not the first conditional clause
					if (!string.IsNullOrEmpty(taxonomyWhere))
					{
						taxonomyWhere += " AND ";
					}

					// Name of the taxonomy parameter
					var taxonomyNameParameter = $"@taxonomy{taxonomyNameIndex}";
					taxonomyParameters.Add(taxonomyNameParameter, taxonomy.Name);

					// Add SQL commands to lookup taxonomy terms
					var subQuery = "SELECT COUNT(1) ";
					subQuery += $"FROM {__(T.TermRelationship)} ";
					subQuery += $"INNER JOIN {__(T.TermTaxonomy)} ON {__(T.TermRelationship, tr => tr.TermTaxonomyId)} = {__(T.TermTaxonomy, tx => tx.TermTaxonomyId)} ";
					subQuery += $"WHERE {__(T.TermTaxonomy, tx => tx.Taxonomy)} = {taxonomyNameParameter} ";
					subQuery += $"AND {__(T.TermRelationship, tr => tr.PostId)} = {__(T.Post, p => p.PostId)} ";
					subQuery += $"AND {__(T.TermTaxonomy, tx => tx.TermId)} IN (";

					// Add the terms for this taxonomy
					var taxonomyIdIndex = 0;
					foreach (var taxonomyId in taxonomy.Ids)
					{
						// Add a comma if this is not the first term
						if (taxonomyIdIndex > 0)
						{
							subQuery += ", ";
						}

						// Add the term parameter and reference
						var taxonomyIdParameter = $"{taxonomyNameParameter}_{taxonomyIdIndex}";

						subQuery += taxonomyIdParameter;
						taxonomyParameters.Add(taxonomyIdParameter, taxonomyId);

						// Increase taxonomy term index
						taxonomyIdIndex++;
					}

					// Close IN function
					subQuery += ")";

					// Add to sub-query, matching the number of terms
					taxonomyWhere += $"({subQuery}) = {taxonomy.Ids.Count}";

					// Increase taxonomy name index
					taxonomyNameIndex++;
				}

				// Add to main WHERE clause
				return AddWhereCustom(parts, taxonomyWhere, taxonomyParameters);
			}

			/// <inheritdoc/>
			public Option<QueryParts> AddWhereCustomFields(QueryParts parts, IImmutableList<(ICustomField, Compare, object)> customFields)
			{
				// If there aren't any, don't do anything
				if (customFields.Count == 0)
				{
					return parts;
				}

				// Setup variables
				var customFieldWhere = string.Empty;
				var customFieldIndex = 0;
				var customFieldParameters = new QueryParameters();

				// Add each custom field
				foreach (var (field, cmp, value) in customFields)
				{
					// Add AND if this is not the first conditional clause
					if (!string.IsNullOrEmpty(customFieldWhere))
					{
						customFieldWhere += " AND ";
					}

					// Ensure there is a search value
					var customFieldSearch = value.ToString();
					if (customFieldSearch == null)
					{
						continue;
					}

					// Set comparison operators and modify search string accordingly
					var customFieldComparison = "=";

					if (cmp == Compare.Like)
					{
						// Change the comparison
						customFieldComparison = "LIKE";

						// If % has not already been added to the search string, add it
						if (!customFieldSearch.Contains("%", StringComparison.CurrentCulture))
						{
							customFieldSearch = $"%{customFieldSearch}%";
						}
					}

					// Name of the custom field parameter
					var customFieldKeyParameter = $"@customField{customFieldIndex}_Key";
					var customFieldValueParameter = $"@customField{customFieldIndex}_Value";
					customFieldParameters.Add(customFieldKeyParameter, field.Key);
					customFieldParameters.Add(customFieldValueParameter, customFieldSearch);

					// Add SQL commands to lookup custom field
					var subQuery = "SELECT COUNT(1) ";
					subQuery += $"FROM {__(T.PostMeta)} ";
					subQuery += $"WHERE {__(T.PostMeta, pm => pm.PostId)} = {__(T.Post, p => p.PostId)} ";
					subQuery += $"AND {__(T.PostMeta, pm => pm.Key)} = {customFieldKeyParameter} ";
					subQuery += $"AND {__(T.PostMeta, pm => pm.Value)} {customFieldComparison} {customFieldValueParameter} ";

					// Add sub query to where
					customFieldWhere += $"({subQuery}) = 1";

					// Increase custom field index
					customFieldIndex++;
				}

				// Add to main WHERE clause
				return AddWhereCustom(parts, customFieldWhere, customFieldParameters);
			}
		}
	}
}
