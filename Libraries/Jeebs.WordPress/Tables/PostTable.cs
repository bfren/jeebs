using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Post Table
	/// </summary>
	public sealed class PostTable : Table
	{
		/// <summary>
		/// PostId
		/// </summary>
		public string PostId { get; } = "ID";

		/// <summary>
		/// AuthorId
		/// </summary>
		public string AuthorId { get; } = "post_author";

		/// <summary>
		/// PublishedOn
		/// </summary>
		public string PublishedOn { get; } = "post_date";

		/// <summary>
		/// PublishedOnGmt
		/// </summary>
		public string PublishedOnGmt { get; } = "post_date_gmt";

		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; } = "post_content";

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; } = "post_title";

		/// <summary>
		/// Excerpt
		/// </summary>
		public string Excerpt { get; } = "post_excerpt";

		/// <summary>
		/// Status
		/// </summary>
		public string Status { get; } = "post_status";

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; } = "post_name";

		/// <summary>
		/// LastModifiedOn
		/// </summary>
		public string LastModifiedOn { get; } = "post_modified";

		/// <summary>
		/// LastModifiedOnGmt
		/// </summary>
		public string LastModifiedOnGmt { get; } = "post_modified_gmt";

		/// <summary>
		/// ParentId
		/// </summary>
		public string ParentId { get; } = "post_parent";

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; } = "guid";

		/// <summary>
		/// Type
		/// </summary>
		public string Type { get; } = "post_type";

		/// <summary>
		/// MimeType
		/// </summary>
		public string MimeType { get; } = "post_mime_type";

		/// <summary>
		/// CommentsCount
		/// </summary>
		public string CommentsCount { get; } = "comment_count";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public PostTable(string prefix) : base($"{prefix}posts") { }
	}
}
