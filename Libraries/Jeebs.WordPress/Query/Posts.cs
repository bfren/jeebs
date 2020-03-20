using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress
{
	public static partial class Query
	{
		public sealed class Posts : Base<PostsOptions>
		{
			/// <summary>
			/// Create object
			/// </summary>
			/// <param name="wpDb">IWpDb</param>
			/// <param name="unitOfWork">[Optional] IUnitOfWork</param>
			public Posts(IWpDb wpDb, IUnitOfWork? unitOfWork = null) : base(wpDb, unitOfWork) { }

			/// <summary>
			/// Build the query using default options
			/// </summary>
			/// <typeparam name="T">Entity type</typeparam>
			/// <param name="modifyOptions">[Optional] Action to modify default options</param>
			public override void Build<T>(Action<PostsOptions>? modifyOptions = null)
			{
				// Get query options
				var opt = new PostsOptions();
				modifyOptions?.Invoke(opt);

				// Get shorthands
				var _ = WpDb;
				var w = UnitOfWork;

				// Get table names
				var p = _.Post.ToString();
				var pm = _.PostMeta.ToString();
				var tr = _.TermRelationship.ToString();
				var tx = _.TermTaxonomy.ToString();

				// SELECT columns FROM table
				AddSelect($"{w.Extract<T>(_.Post)} AS '{p}' FROM {w.Escape(_.Post)}");

				// WHERE type
				var type = opt.Type;
				AddWhere($"{w.Escape(p, _.Post.Type)} = @{nameof(type)}", new { type });

				// WHERE status
				var status = opt.Status;
				AddWhere($"{w.Escape(p, _.Post.Status)} = @{nameof(status)}", new { status });

				// WHERE Id
				if (opt.Id is double postId)
				{
					AddWhere($"{w.Escape(p, _.Post.PostId)} = @{nameof(postId)}", new { postId });
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
						where += $"{w.Escape(p,_.Post.Title)} {comparison} @{nameof(search)}";
					}

					// Search slug
					if ((opt.SearchFields & SearchPostFields.Slug) != 0)
					{
						if (!string.IsNullOrEmpty(where))
						{
							where += " OR ";
						}

						where += $"{w.Escape(p, _.Post.Slug)} {comparison} @{nameof(search)}";
					}

					// Search content
					if ((opt.SearchFields & SearchPostFields.Content) != 0)
					{
						if (!string.IsNullOrEmpty(where))
						{
							where += " OR ";
						}

						where += $"{w.Escape(p, _.Post.Content)} {comparison} @{nameof(search)}";
					}

					// Add to WHERE
					AddWhere(where, new { search });
				}

				// WHERE dates
				if (opt.From is DateTime f)
				{
					var from = f.StartOfDay().ToMySqlString();
					AddWhere($"{w.Escape(p, _.Post.PublishedOn)} >= @{nameof(from)}", new { from });
				}
				if (opt.To is DateTime t)
				{
					var to = t.EndOfDay().ToMySqlString();
					AddWhere($"{w.Escape(p, _.Post.PublishedOn)} <= @{nameof(to)}", new { to });
				}

				// WHERE parent ID
				if (opt.ParentId is int parentId)
				{
					AddWhere($"{w.Escape(p, _.Post.ParentId)} = @{nameof(parentId)}", new { parentId });
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

				// LIMIT
				AddLimit(opt.Limit);

				// OFFSET
				AddOffset(opt.Offset);
			}
		}
	}
}
