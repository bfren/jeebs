using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Mapping;
using Jeebs.WordPress.Enums;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Post entity
	/// </summary>
	public abstract record WpPostEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public long Id
		{
			get =>
				PostId;

			set =>
				PostId = value;
		}

		/// <summary>
		/// PostId
		/// </summary>
		[Id]
		public long PostId { get; set; }

		/// <summary>
		/// AuthorId
		/// </summary>
		public long AuthorId { get; set; }

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
		public string Content { get; set; } = string.Empty;

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Excerpt
		/// </summary>
		public string Excerpt { get; set; } = string.Empty;

		/// <summary>
		/// Status
		/// </summary>
		public PostStatus Status { get; set; } = PostStatus.Draft;

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; set; } = string.Empty;

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
		public long ParentId { get; set; }

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; set; } = string.Empty;

		/// <summary>
		/// Type
		/// </summary>
		public PostType Type { get; set; } = PostType.Post;

		/// <summary>
		/// MimeType
		/// </summary>
		public MimeType MimeType { get; set; } = MimeType.Blank;

		/// <summary>
		/// CommentsCount
		/// </summary>
		public long CommentsCount { get; set; }
	}
}
