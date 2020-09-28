﻿using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress
{
	/// <summary>
	/// Query Posts
	/// </summary>
	public partial class QueryPosts
	{
		/// <inheritdoc/>
		public sealed class Options : Data.Querying.QueryOptions
		{
			/// <summary>
			/// Search Post type
			/// </summary>
			public PostType Type { get; set; } = PostType.Post;

			/// <summary>
			/// Search Post status
			/// </summary>
			public PostStatus Status { get; set; } = PostStatus.Publish;

			/// <summary>
			/// Search Post Text
			/// </summary>
			public string? SearchText { get; set; }

			/// <summary>
			/// Search text fields
			/// </summary>
			public SearchPostFields SearchFields { get; set; } = SearchPostFields.All;

			/// <summary>
			/// Search text operator (= or LIKE)
			/// </summary>
			public SearchOperators SearchOperator { get; set; } = SearchOperators.Like;

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

			/// <summary>
			/// Search post taxonomies
			/// </summary>
			public IList<(Taxonomy taxonomy, long id)> Taxonomies { get; set; } = new List<(Taxonomy, long)>();

			/// <summary>
			/// Search custom fields
			/// </summary>
			public IList<(ICustomField field, SearchOperators op, object value)> CustomFields { get; set; } = new List<(ICustomField, SearchOperators, object)>();
		}
	}
}
