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
	public abstract record WpPostEntity : IEntity, IEntity<long>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		long IEntity.Id =>
			Id.Value;

		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public IStrongId<long> Id
		{
			get =>
				new WpPostId(PostId);

			init =>
				PostId = value.Value;
		}

		/// <summary>
		/// PostId
		/// </summary>
		[Id]
		public long PostId { get; init; }

		/// <summary>
		/// AuthorId
		/// </summary>
		public long AuthorId { get; init; }

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
		public long ParentId { get; init; }

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
		public long CommentsCount { get; init; }
	}
}
