using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Posts
	/// </summary>
	public partial class Posts
	{
		/// <summary>
		/// Query Builder
		/// </summary>
		internal sealed class QueryBuilder : QueryBuilder<QueryOptions>
		{
			/// <summary>
			/// IWpDb
			/// </summary>
			private readonly IWpDb db;

			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="db">IWpDb</param>
			internal QueryBuilder(IWpDb db) : base(db.Adapter) => this.db = db;

			/// <summary>
			/// Build query
			/// </summary>
			/// <typeparam name="T">Entity type</typeparam>
			/// <param name="opt">QueryOptions</param>
			public override QueryArgs Build<T>(QueryOptions opt)
			{
				// Use db shorthand
				var _ = db;

				// Get table names
				var p = _.Post.ToString();
				var pm = _.PostMeta.ToString();
				var tr = _.TermRelationship.ToString();
				var tx = _.TermTaxonomy.ToString();

				// SELECT columns
				AddSelect($"{Extract<T>(_.Post)}");

				// FROM table
				AddFrom($"{Escape(_.Post)} AS {Escape(p)}");

				// WHERE type
				var type = opt.Type;
				AddWhere($"{Escape(p, _.Post.Type)} = @{nameof(type)}", new { type });

				// WHERE status
				var status = opt.Status;
				AddWhere($"{Escape(p, _.Post.Status)} = @{nameof(status)}", new { status });

				// WHERE Id
				if (opt.Id is double postId)
				{
					AddWhere($"{Escape(p, _.Post.PostId)} = @{nameof(postId)}", new { postId });
				}

				// WHERE search
				else if (opt.SearchText is string searchText)
				{
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
						where += $"{Escape(p, _.Post.Title)} {comparison} @{nameof(search)}";
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

				// WHERE dates
				if (opt.From is DateTime f)
				{
					var from = f.StartOfDay().ToMySqlString();
					AddWhere($"{Escape(p, _.Post.PublishedOn)} >= @{nameof(from)}", new { from });
				}
				if (opt.To is DateTime t)
				{
					var to = t.EndOfDay().ToMySqlString();
					AddWhere($"{Escape(p, _.Post.PublishedOn)} <= @{nameof(to)}", new { to });
				}

				// WHERE parent ID
				if (opt.ParentId is int parentId)
				{
					AddWhere($"{Escape(p, _.Post.ParentId)} = @{nameof(parentId)}", new { parentId });
				}

				// ORDER BY
				if (opt.SortRandom)
				{
					AddOrderByRandom();
				}
				else if (opt.Sort is (string selectColumn, SortOrder order)[] sort)
				{
					AddOrderBy(sort);
				}
				else
				{
					AddOrderBy((_.Post.PublishedOn, SortOrder.Descending));
				}

				// Return
				return base.Build<T>(opt);
			}
		}
	}
}
