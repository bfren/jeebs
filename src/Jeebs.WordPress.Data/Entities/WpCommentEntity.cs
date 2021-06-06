// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs.Data.Entities;
using Jeebs.WordPress.Data.Enums;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// Comment entity
	/// </summary>
	public abstract record WpCommentEntity : IWithId<WpCommentId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpCommentId Id
		{
			get =>
				new(CommentId);

			init =>
				CommentId = value.Value;
		}

		/// <summary>
		/// CommentId
		/// </summary>
		[Id]
		public long CommentId { get; init; }

		/// <summary>
		/// PostId
		/// </summary>
		public long PostId { get; init; }

		/// <summary>
		/// AuthorName
		/// </summary>
		public string AuthorName { get; init; } = string.Empty;

		/// <summary>
		/// AuthorEmail
		/// </summary>
		public string AuthorEmail { get; init; } = string.Empty;

		/// <summary>
		/// AuthorUrl
		/// </summary>
		public string AuthorUrl { get; init; } = string.Empty;

		/// <summary>
		/// AuthorIp
		/// </summary>
		public string AuthorIp { get; init; } = string.Empty;

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
		/// Karma
		/// </summary>
		public long Karma { get; init; }

		/// <summary>
		/// IsApproved
		/// </summary>
		public bool IsApproved { get; init; }

		/// <summary>
		/// AuthorUserAgent
		/// </summary>
		public string AuthorUserAgent { get; init; } = string.Empty;

		/// <summary>
		/// Type
		/// </summary>
		public CommentType Type { get; init; } = CommentType.Blank;

		/// <summary>
		/// ParentId
		/// </summary>
		public long ParentId { get; init; }

		/// <summary>
		/// AuthorId
		/// </summary>
		public long AuthorId { get; init; }

		/// <summary>
		/// AuthorIsSubscribed
		/// </summary>
		public bool AuthorIsSubscribed { get; init; }
	}
}
