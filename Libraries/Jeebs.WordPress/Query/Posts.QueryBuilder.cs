using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Posts
	/// </summary>
	public partial class QueryPosts
	{
		/// <summary>
		/// Query Builder
		/// </summary>
		internal sealed class Builder<T> : QueryPartsBuilder<T, Options>
		{
			/// <summary>
			/// IWpDb
			/// </summary>
			private readonly IWpDb db;

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal Builder(IWpDb db) : base(db.Adapter) => this.db = db;

			/// <summary>
			/// Build query
			/// </summary>
			/// <param name="opt">QueryOptions</param>
			public override IQueryParts<T> Build(Options opt)
			{
				// Use db shorthands
				var _ = db;
				var p = _.Post.ToString();

				// SELECT columns
				AddSelect($"{Adapter.Extract<T>(_.Post)}");

				// FROM table
				AddFrom($"{Escape(_.Post)} AS {Escape(p)}");

				// WHERE type
				var type = opt.Type;
				AddWhere($"{Escape(p, _.Post.Type)} = @{nameof(type)}", new { type });

				// WHERE status
				var status = opt.Status;
				AddWhere($"{Escape(p, _.Post.Status)} = @{nameof(status)}", new { status });

				// WHERE Id
				if (opt.Id is long postId)
				{
					AddWhere($"{Escape(p, _.Post.PostId)} = @{nameof(postId)}", new { postId });
				}

				// WHERE search
				else if (opt.SearchText is string searchText)
				{
					AddWhereSearch(searchText, opt);
				}

				// WHERE dates
				if (opt.From is DateTime fromBase)
				{
					var from = fromBase.StartOfDay().ToMySqlString();
					AddWhere($"{Escape(p, _.Post.PublishedOn)} >= @{nameof(from)}", new { from });
				}
				if (opt.To is DateTime toBase)
				{
					var to = toBase.EndOfDay().ToMySqlString();
					AddWhere($"{Escape(p, _.Post.PublishedOn)} <= @{nameof(to)}", new { to });
				}

				// WHERE parent ID
				if (opt.ParentId is int parentId)
				{
					AddWhere($"{Escape(p, _.Post.ParentId)} = @{nameof(parentId)}", new { parentId });
				}

				// WHERE taxonomies
				if (opt.Taxonomies is IList<(Taxonomy taxonomy, int id)> taxonomiesList && taxonomiesList.Count > 0)
				{
					AddWhereTaxonomies(taxonomiesList);
				}

				// WHERE custom fields
				if (opt.CustomFields is IList<(ICustomField field, SearchOperators op, object value)> fields && fields.Count > 0)
				{
					AddWhereCustomFields(fields);
				}

				// Finish and Return
				return FinishBuild(opt, (Escape(p, _.Post.PublishedOn), SortOrder.Descending));
			}

			/// <summary>
			/// Add WHERE for text search
			/// </summary>
			/// <param name="searchText">Search text</param>
			/// <param name="opt">QueryOptions</param>
			private void AddWhereSearch(string searchText, Options opt)
			{
				// Use shorthands
				var _ = db;
				var p = _.Post.ToString();

				// Trim search text
				var search = searchText.Trim();
				var where = string.Empty;

				// Set comparison operator and modify search string accordingly
				var comparison = "=";
				if (opt.SearchOperator == SearchOperators.Like)
				{
					// Change the comparison
					comparison = "LIKE";

					// If % has not already been added to the search string, add it
					if (search.IndexOf("%") == -1)
					{
						search = $"%{search}%";
					}
				}

				// Search title
				if ((opt.SearchFields & SearchPostFields.Title) != 0)
				{
					where += $"{Escape(p, db.Post.Title)} {comparison} @{nameof(search)}";
				}

				// Search slug
				if ((opt.SearchFields & SearchPostFields.Slug) != 0)
				{
					if (!string.IsNullOrEmpty(where))
					{
						where += " OR ";
					}

					where += $"{Escape(p, _.Post.Slug)} {comparison} @{nameof(search)}";
				}

				// Search content
				if ((opt.SearchFields & SearchPostFields.Content) != 0)
				{
					if (!string.IsNullOrEmpty(where))
					{
						where += " OR ";
					}

					where += $"{Escape(p, _.Post.Content)} {comparison} @{nameof(search)}";
				}

				// Add to WHERE
				AddWhere($"({where})", new { search });
			}

			/// <summary>
			/// Add WHERE for taxonomies search
			/// </summary>
			/// <param name="taxonomiesList">List of taxonomies to search</param>
			private void AddWhereTaxonomies(IList<(Taxonomy taxonomy, int id)> taxonomiesList)
			{
				// Use shorthands
				var _ = db;
				var p = _.Post.ToString();
				var tr = _.TermRelationship.ToString();
				var tx = _.TermTaxonomy.ToString();

				// Setup variables
				var taxonomyWhere = string.Empty;
				var taxonomyNameIndex = 0;
				var taxonomyParameters = new QueryParameters();

				// Group taxonomies by taxonomy name
				var taxonomies = from t in taxonomiesList
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
					subQuery += $"FROM {Escape(tr)} ";
					subQuery += $"INNER JOIN {Escape(tx)} ON {Escape(tr, _.TermRelationship.TermTaxonomyId)} = {Escape(tx, _.TermTaxonomy.TermTaxonomyId)} ";
					subQuery += $"WHERE {Escape(tx, _.TermTaxonomy.Taxonomy)} = {taxonomyNameParameter} ";
					subQuery += $"AND {Escape(tr, _.TermRelationship.PostId)} = {Escape(p, _.Post.PostId)} ";
					subQuery += $"AND {Escape(tx, _.TermTaxonomy.TermId)} IN (";

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
				if (!string.IsNullOrEmpty(taxonomyWhere))
				{
					AddWhere($"({taxonomyWhere})", taxonomyParameters);
				}
			}

			/// <summary>
			/// Add WHERE for custom fields search
			/// </summary>
			/// <param name="fields">List of custom fields to search</param>
			private void AddWhereCustomFields(IList<(ICustomField field, SearchOperators op, object value)> fields)
			{
				// Use shorthands
				var _ = db;
				var p = _.Post.ToString();
				var pm = _.PostMeta.ToString();

				// Setup variables
				var customFieldWhere = string.Empty;
				var customFieldIndex = 0;
				var customFieldParameters = new QueryParameters();

				// Add each custom field
				foreach (var (field, op, value) in fields)
				{
					// Add AND if this is not the first conditional clause
					if (!string.IsNullOrEmpty(customFieldWhere))
					{
						customFieldWhere += " AND ";
					}

					// Set comparison operators and modify search string accordingly
					var customFieldComparison = "=";
					var customFieldSearch = value.ToString();
					if (op == SearchOperators.Like)
					{
						// Change the comparison
						customFieldComparison = "LIKE";

						// If % has not already been added to the search string, add it
						if (customFieldSearch.IndexOf("%") == -1)
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
					subQuery += $"FROM {Escape(pm)} ";
					subQuery += $"WHERE {Escape(pm, _.PostMeta.PostId)} = {Escape(p, _.Post.PostId)} ";
					subQuery += $"AND {Escape(pm, _.PostMeta.Key)} = {customFieldKeyParameter} ";
					subQuery += $"AND {Escape(pm, _.PostMeta.Value)} {customFieldComparison} {customFieldValueParameter} ";

					// Add sub query to where
					customFieldWhere += $"({subQuery}) = 1";

					// Increase custom field index
					customFieldIndex++;
				}

				// Add to main WHERE clause
				if (!string.IsNullOrEmpty(customFieldWhere))
				{
					AddWhere($"({customFieldWhere})", customFieldParameters);
				}
			}
		}
	}
}
