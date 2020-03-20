using System;
using System.Collections.Generic;
using System.Text;
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
		/// Query Options
		/// </summary>
		public sealed class QueryOptions : Data.QueryOptions
		{
			/// <summary>
			/// Search Post type
			/// </summary>
			public PostType Type { get; set; }

			/// <summary>
			/// Search Post status
			/// </summary>
			public PostStatus Status { get; set; }

			/// <summary>
			/// Search Post Text
			/// </summary>
			public string? SearchText { get; set; }

			/// <summary>
			/// Search text fields
			/// </summary>
			public SearchPostFields SearchFields { get; set; }

			/// <summary>
			/// Search text operator (= or LIKE)
			/// </summary>
			public SearchOperators SearchOperator { get; set; }

			/// <summary>
			/// Search Post published from
			/// </summary>
			public DateTime? From { get; set; }

			/// <summary>
			/// Search Post published to
			/// </summary>
			public DateTime? To { get; set; }

			/// <summary>
			/// Parent ID
			/// </summary>
			public int? ParentId { get; set; }

			public QueryOptions()
			{
				Type = PostType.Post;
				Status = PostStatus.Publish;
				SearchFields = SearchPostFields.Title | SearchPostFields.Slug | SearchPostFields.Content;
				SearchOperator = SearchOperators.Like;
				//CustomFields = new List<(ICustomFieldQueryable field, SearchOperators op, object value)>();
				//Taxonomies = new List<(Taxonomy, int)>();
			}
		}
	}
}
