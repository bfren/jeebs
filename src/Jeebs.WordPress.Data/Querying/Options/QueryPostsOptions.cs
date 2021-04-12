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

namespace Jeebs.WordPress.Data.Querying
{
	/// <inheritdoc cref="IQueryPostsOptions{TEntity}"/>
	public sealed record QueryPostsOptions<TEntity> : QueryOptions<TEntity, WpPostId>, IQueryPostsOptions<TEntity>
		where TEntity : WpPostEntity
	{
		/// <inheritdoc/>
		public PostType Type { get; init; } = PostType.Post;

		/// <inheritdoc/>
		public PostStatus Status { get; init; } = PostStatus.Publish;

		/// <inheritdoc/>
		public string? SearchText { get; init; }

		/// <inheritdoc/>
		public SearchPostFields SearchFields { get; init; } = SearchPostFields.All;

		/// <inheritdoc/>
		public Compare SearchComparison { get; init; } = Compare.Like;

		/// <inheritdoc/>
		public DateTime? From { get; init; }

		/// <inheritdoc/>
		public DateTime? To { get; init; }

		/// <inheritdoc/>
		public int? ParentId { get; init; }

		/// <inheritdoc/>
		public IImmutableList<(Taxonomy taxonomy, long id)> Taxonomies { get; init; } =
			new ImmutableList<(Taxonomy taxonomy, long id)>();

		/// <inheritdoc/>
		public IImmutableList<(ICustomField field, Compare cmp, object value)> CustomFields { get; init; } =
			new ImmutableList<(ICustomField field, Compare cmp, object value)>();

		/// <summary>
		/// Internal creation only
		/// </summary>
		/// <param name="db">IWpDb</param>
		internal QueryPostsOptions(IWpDb db) : base(db) { }

		/// <inheritdoc/>
		protected override Option<QueryParts> GetParts(ITableMap map, IColumnList cols) =>
			base.GetParts(
				map, cols
			)
			.Bind(
				AddWhereType
			)
			.Bind(
				AddWhereStatus
			)
			.SwitchIf(
				_ => SearchText is not null,
				AddWhereSearch
			)
			.SwitchIf(
				_ => From is not null,
				AddWherePublishedFrom
			)
			.SwitchIf(
				_ => To is not null,
				AddWherePublishedTo
			)
			.SwitchIf(
				_ => ParentId is not null,
				AddWhereParentId
			)
			.SwitchIf(
				_ => Taxonomies.Count > 0,
				AddWhereTaxonomies
			)
			.SwitchIf(
				_ => CustomFields.Count > 0,
				AddWhereCustomFields
			);

		/// <summary>
		/// Add Where Post Type
		/// </summary>
		/// <param name="parts">QueryParts</param>
		internal Option<QueryParts> AddWhereType(QueryParts parts) =>
			AddWhere(parts, Db.Post, p => p.Type, Compare.Equal, Type);

		/// <summary>
		/// Add Where Post Status
		/// </summary>
		/// <param name="parts">QueryParts</param>
		internal Option<QueryParts> AddWhereStatus(QueryParts parts) =>
			AddWhere(parts, Db.Post, p => p.Status, Compare.Equal, Status);

		/// <summary>
		/// Add Where Search
		/// </summary>
		/// <param name="parts">QueryParts</param>
		internal Option<QueryParts> AddWhereSearch(QueryParts parts)
		{
			// If there isn't any search text, don't do anything
			if (SearchText is null)
			{
				return parts;
			}

			// Create objects
			var clause = new StringBuilder();

			// Trim search text
			var search = SearchText.Trim();

			// Set comparison operator and modify search string accordingly
			var comparison = "=";
			if (SearchComparison == Compare.Like)
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
			if ((SearchFields & SearchPostFields.Title) != 0)
			{
				clause.Append($"{__(Db.Post, p => p.Title)} {comparison} @{nameof(search)}");
			}

			// Search slug
			if ((SearchFields & SearchPostFields.Slug) != 0)
			{
				if (clause.Length > 0)
				{
					clause.Append(" OR ");
				}

				clause.Append($"{__(Db.Post, p => p.Slug)} {comparison} @{nameof(search)}");
			}

			// Search content
			if ((SearchFields & SearchPostFields.Content) != 0)
			{
				if (clause.Length > 0)
				{
					clause.Append(" OR ");
				}

				clause.Append($"{__(Db.Post, p => p.Content)} {comparison} @{nameof(search)}");
			}

			// Search excerpt
			if ((SearchFields & SearchPostFields.Excerpt) != 0)
			{
				if (clause.Length > 0)
				{
					clause.Append(" OR ");
				}

				clause.Append($"{__(Db.Post, p => p.Excerpt)} {comparison} @{nameof(search)}");
			}

			// Return
			return AddWhereCustom(parts, clause.ToString(), new { search });
		}

		/// <summary>
		/// Add Where From / To
		/// </summary>
		/// <param name="parts">QueryParts</param>
		internal Option<QueryParts> AddWherePublishedFrom(QueryParts parts)
		{
			// Add From (use start of the day)
			if (From is DateTime fromBase)
			{
				var from = fromBase.StartOfDay().ToMySqlString();
				return AddWhere(parts, Db.Post, p => p.PublishedOn, Compare.MoreThanOrEqual, from);
			}

			// Return
			return parts;
		}

		/// <summary>
		/// Add Where From / To
		/// </summary>
		/// <param name="parts">QueryParts</param>
		internal Option<QueryParts> AddWherePublishedTo(QueryParts parts)
		{
			// Add To (use end of the day)
			if (To is DateTime toBase)
			{
				var to = toBase.EndOfDay().ToMySqlString();
				return AddWhere(parts, Db.Post, p => p.PublishedOn, Compare.LessThanOrEqual, to);
			}

			// Return
			return parts;
		}

		/// <summary>
		/// Add Where Parent ID
		/// </summary>
		/// <param name="parts">QueryParts</param>
		internal Option<QueryParts> AddWhereParentId(QueryParts parts)
		{
			// Add Parent ID
			if (ParentId is int parentId)
			{
				return AddWhere(parts, Db.Post, p => p.ParentId, Compare.Equal, parentId);
			}

			// Return
			return parts;
		}

		/// <summary>
		/// Add Where Taxonomies
		/// </summary>
		/// <param name="parts">QueryParts</param>
		internal Option<QueryParts> AddWhereTaxonomies(QueryParts parts)
		{
			// If there aren't any, don't do anything
			if (Taxonomies.Count == 0)
			{
				return parts;
			}

			// Setup variables
			var taxonomyWhere = string.Empty;
			var taxonomyNameIndex = 0;
			var taxonomyParameters = new QueryParameters();

			// Group taxonomies by taxonomy name
			var taxonomies = from t in Taxonomies
							 group t by t.taxonomy into g
							 select new
							 {
								 Name = g.Key,
								 Ids = g.Select(x => x.id).ToList()
							 };

			// Add each taxonomy
			foreach (var taxonomy in taxonomies)
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
				subQuery += $"FROM {__(Db.TermRelationship)} ";
				subQuery += $"INNER JOIN {__(Db.TermTaxonomy)} ON {__(Db.TermRelationship, tr => tr.TermTaxonomyId)} = {__(Db.TermTaxonomy, tx => tx.TermTaxonomyId)} ";
				subQuery += $"WHERE {__(Db.TermTaxonomy, tx => tx.Taxonomy)} = {taxonomyNameParameter} ";
				subQuery += $"AND {__(Db.TermRelationship, tr => tr.PostId)} = {__(Db.Post, p => p.PostId)} ";
				subQuery += $"AND {__(Db.TermTaxonomy, tx => tx.TermId)} IN (";

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

		/// <summary>
		/// Add Where Custom Fields
		/// </summary>
		/// <param name="parts">QueryParts</param>
		internal Option<QueryParts> AddWhereCustomFields(QueryParts parts)
		{
			// If there aren't any, don't do anything
			if (CustomFields.Count == 0)
			{
				return parts;
			}

			// Setup variables
			var customFieldWhere = string.Empty;
			var customFieldIndex = 0;
			var customFieldParameters = new QueryParameters();

			// Add each custom field
			foreach (var (field, cmp, value) in CustomFields)
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
				subQuery += $"FROM {__(Db.PostMeta)} ";
				subQuery += $"WHERE {__(Db.PostMeta, pm => pm.PostId)} = {__(Db.Post, p => p.PostId)} ";
				subQuery += $"AND {__(Db.PostMeta, pm => pm.Key)} = {customFieldKeyParameter} ";
				subQuery += $"AND {__(Db.PostMeta, pm => pm.Value)} {customFieldComparison} {customFieldValueParameter} ";

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
