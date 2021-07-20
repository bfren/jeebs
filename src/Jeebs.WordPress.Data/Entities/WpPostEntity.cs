// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// Post entity
	/// </summary>
	public abstract record WpPostEntity : WpPostEntityWithId
	{
		/// <summary>
		/// AuthorId
		/// </summary>
		public WpUserId AuthorId { get; init; } = new();

		/// <summary>
		/// PublishedOn
		/// </summary>
		public DateTime PublishedOn { get; init; }

		/// <summary>
		/// PublishedOnGmt
		/// </summary>
		public DateTime PublishedOnGmt { get; init; }

		/// <summary>
		/// Content
		/// </summary>
		public string Content { get; init; } = string.Empty;

		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; init; } = string.Empty;

		/// <summary>
		/// Excerpt
		/// </summary>
		public string Excerpt { get; init; } = string.Empty;

		/// <summary>
		/// Status
		/// </summary>
		public PostStatus Status { get; init; } = PostStatus.Draft;

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; init; } = string.Empty;

		/// <summary>
		/// LastModifiedOn
		/// </summary>
		public DateTime LastModifiedOn { get; init; }

		/// <summary>
		/// LastModifiedOnGmt
		/// </summary>
		public DateTime LastModifiedOnGmt { get; init; }

		/// <summary>
		/// ParentId
		/// </summary>
		public WpPostId ParentId { get; init; } = new();

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; init; } = string.Empty;

		/// <summary>
		/// Type
		/// </summary>
		public PostType Type { get; init; } = PostType.Post;

		/// <summary>
		/// MimeType
		/// </summary>
		public MimeType MimeType { get; init; } = MimeType.Blank;

		/// <summary>
		/// CommentsCount
		/// </summary>
		public ulong CommentsCount { get; init; }
	}
}
