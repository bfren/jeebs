// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Mapping;

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
		public string PostId =>
			"ID";

		/// <summary>
		/// AuthorId
		/// </summary>
		public string AuthorId =>
			"post_author";

		/// <summary>
		/// PublishedOn
		/// </summary>
		public string PublishedOn =>
			"post_date";

		/// <summary>
		/// PublishedOnGmt
		/// </summary>
		public string PublishedOnGmt =>
			"post_date_gmt";

		/// <summary>
		/// Content
		/// </summary>
		public string Content =>
			"post_content";

		/// <summary>
		/// Title
		/// </summary>
		public string Title =>
			"post_title";

		/// <summary>
		/// Excerpt
		/// </summary>
		public string Excerpt =>
			"post_excerpt";

		/// <summary>
		/// Status
		/// </summary>
		public string Status =>
			"post_status";

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug =>
			"post_name";

		/// <summary>
		/// LastModifiedOn
		/// </summary>
		public string LastModifiedOn =>
			"post_modified";

		/// <summary>
		/// LastModifiedOnGmt
		/// </summary>
		public string LastModifiedOnGmt =>
			"post_modified_gmt";

		/// <summary>
		/// ParentId
		/// </summary>
		public string ParentId =>
			"post_parent";

		/// <summary>
		/// Url
		/// </summary>
		public string Url =>
			"guid";

		/// <summary>
		/// Type
		/// </summary>
		public string Type =>
			"post_type";

		/// <summary>
		/// MimeType
		/// </summary>
		public string MimeType =>
			"post_mime_type";

		/// <summary>
		/// CommentsCount
		/// </summary>
		public string CommentsCount =>
			"comment_count";

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="prefix">Table prefix</param>
		public PostTable(string prefix) : base($"{prefix}posts") { }
	}
}
