using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;

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
		public readonly string PostId = "ID";

		/// <summary>
		/// AuthorId
		/// </summary>
		public readonly string AuthorId = "post_author";

		/// <summary>
		/// PublishedOn
		/// </summary>
		public readonly string PublishedOn = "post_date";

		/// <summary>
		/// PublishedOnGmt
		/// </summary>
		public readonly string PublishedOnGmt = "post_date_gmt";

		/// <summary>
		/// Content
		/// </summary>
		public readonly string Content = "post_content";

		/// <summary>
		/// Title
		/// </summary>
		public readonly string Title = "post_title";

		/// <summary>
		/// Excerpt
		/// </summary>
		public readonly string Excerpt = "post_excerpt";

		/// <summary>
		/// Status
		/// </summary>
		public readonly string Status = "post_status";

		/// <summary>
		/// Slug
		/// </summary>
		public readonly string Slug = "post_name";

		/// <summary>
		/// LastModifiedOn
		/// </summary>
		public readonly string LastModifiedOn = "post_modified";

		/// <summary>
		/// LastModifiedOnGmt
		/// </summary>
		public readonly string LastModifiedOnGmt = "post_modified_gmt";

		/// <summary>
		/// ParentId
		/// </summary>
		public readonly string ParentId = "post_parent";

		/// <summary>
		/// Url
		/// </summary>
		public readonly string Url = "guid";

		/// <summary>
		/// Type
		/// </summary>
		public readonly string Type = "post_type";

		/// <summary>
		/// MimeType
		/// </summary>
		public readonly string MimeType = "post_mime_type";

		/// <summary>
		/// CommentsCount
		/// </summary>
		public readonly string CommentsCount = "comment_count";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public PostTable(in string prefix) : base($"{prefix}posts") { }
	}
}
