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
		public readonly string CommentId = "comment_ID";

		/// <summary>
		/// PostId
		/// </summary>
		public readonly string PostId = "comment_post_ID";

		/// <summary>
		/// AuthorName
		/// </summary>
		public readonly string AuthorName = "comment_author";

		/// <summary>
		/// AuthorEmail
		/// </summary>
		public readonly string AuthorEmail = "comment_author_email";

		/// <summary>
		/// AuthorUrl
		/// </summary>
		public readonly string AuthorUrl = "comment_author_url";

		/// <summary>
		/// AuthorIp
		/// </summary>
		public readonly string AuthorIp = "comment_author_IP";

		/// <summary>
		/// PublishedOn
		/// </summary>
		public readonly string PublishedOn = "comment_date";

		/// <summary>
		/// PublishedOnGmt
		/// </summary>
		public readonly string PublishedOnGmt = "comment_date_gmt";

		/// <summary>
		/// Content
		/// </summary>
		public readonly string Content = "comment_content";

		/// <summary>
		/// Karma
		/// </summary>
		public readonly string Karma = "comment_karma";

		/// <summary>
		/// IsApproved
		/// </summary>
		public readonly string IsApproved = "comment_approved";

		/// <summary>
		/// AuthorUserAgent
		/// </summary>
		public readonly string AuthorUserAgent = "comment_agent";

		/// <summary>
		/// Type
		/// </summary>
		public readonly string Type = "comment_type";

		/// <summary>
		/// ParentId
		/// </summary>
		public readonly string ParentId = "comment_parent";

		/// <summary>
		/// AuthorId
		/// </summary>
		public readonly string AuthorId = "user_id";

		/// <summary>
		/// AuthorIsSubscribed
		/// </summary>
		public readonly string AuthorIsSubscribed = "comment_subscribe";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public CommentTable(string prefix) : base($"{prefix}comments") { }
	}
}
