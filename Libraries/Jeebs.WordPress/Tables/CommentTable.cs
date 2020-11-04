using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Tables
{
	/// <summary>
	/// Comment Table
	/// </summary>
	public sealed class CommentTable : Table
	{
		/// <summary>
		/// CommentId
		/// </summary>
		public string CommentId { get; } = "comment_ID";

		/// <summary>
		/// PostId
		/// </summary>
		public string PostId { get; } = "comment_post_ID";

		/// <summary>
		/// AuthorName
		/// </summary>
		public string AuthorName { get; } = "comment_author";

		/// <summary>
		/// AuthorEmail
		/// </summary>
		public string AuthorEmail { get; } = "comment_author_email";

		/// <summary>
		/// AuthorUrl
		/// </summary>
		public string AuthorUrl { get; } = "comment_author_url";

		/// <summary>
		/// AuthorIp
		/// </summary>
		public string AuthorIp { get; } = "comment_author_IP";

		/// <summary>
		/// PublishedOn
		/// </summary>
		public string PublishedOn { get; } = "comment_date";

		/// <summary>
		/// PublishedOnGmt
		/// </summary>
		public string PublishedOnGmt { get; } = "comment_date_gmt";

		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; } = "comment_content";

		/// <summary>
		/// Karma
		/// </summary>
		public string Karma { get; } = "comment_karma";

		/// <summary>
		/// IsApproved
		/// </summary>
		public string IsApproved { get; } = "comment_approved";

		/// <summary>
		/// AuthorUserAgent
		/// </summary>
		public string AuthorUserAgent { get; } = "comment_agent";

		/// <summary>
		/// Type
		/// </summary>
		public string Type { get; } = "comment_type";

		/// <summary>
		/// ParentId
		/// </summary>
		public string ParentId { get; } = "comment_parent";

		/// <summary>
		/// AuthorId
		/// </summary>
		public string AuthorId { get; } = "user_id";

		/// <summary>
		/// AuthorIsSubscribed
		/// </summary>
		public string AuthorIsSubscribed { get; } = "comment_subscribe";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public CommentTable(string prefix) : base($"{prefix}comments") { }
	}
}
