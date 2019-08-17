using Jeebs.WordPress.Enums;
using System;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Post entity
	/// </summary>
	public class WpPostEntity
	{
		/// <summary>
		/// PostId
		/// </summary>
		public int PostId { get; set; }

		/// <summary>
		/// AuthorId
		/// </summary>
		public int AuthorId { get; set; }

		/// <summary>
		/// PublishedOn
		/// </summary>
		public DateTime PublishedOn { get; set; }

		/// <summary>
		/// PublishedOnGmt
		/// </summary>
		public DateTime PublishedOnGmt { get; set; }

		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Excerpt
		/// </summary>
		public string Excerpt { get; set; }

		/// <summary>
		/// Status
		/// </summary>
		public PostStatus Status { get; set; }

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// LastModifiedOn
		/// </summary>
		public DateTime LastModifiedOn { get; set; }

		/// <summary>
		/// LastModifiedOnGmt
		/// </summary>
		public DateTime LastModifiedOnGmt { get; set; }

		/// <summary>
		/// ParentId
		/// </summary>
		public int ParentId { get; set; }

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Type
		/// </summary>
		public PostType Type { get; set; }

		/// <summary>
		/// MimeType
		/// </summary>
		public MimeType MimeType { get; set; }

		/// <summary>
		/// CommentsCount
		/// </summary>
		public int CommentsCount { get; set; }
	}
}
