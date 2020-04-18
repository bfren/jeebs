using Jeebs.Data;
using Jeebs.WordPress.Enums;
using System;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// Comment entity
	/// </summary>
	public abstract class WpCommentEntity : IEntity
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public long Id { get => CommentId; set => CommentId = value; }

		/// <summary>
		/// CommentId
		/// </summary>
		[Id]
		public long CommentId { get; set; }

		/// <summary>
		/// PostId
		/// </summary>
		public long PostId { get; set; }

		/// <summary>
		/// AuthorName
		/// </summary>
		public string AuthorName { get; set; } = string.Empty;

		/// <summary>
		/// AuthorEmail
		/// </summary>
		public string AuthorEmail { get; set; } = string.Empty;

		/// <summary>
		/// AuthorUrl
		/// </summary>
		public string AuthorUrl { get; set; } = string.Empty;

		/// <summary>
		/// AuthorIp
		/// </summary>
		public string AuthorIp { get; set; } = string.Empty;

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
		/// Karma
		/// </summary>
		public long Karma { get; set; }

		/// <summary>
		/// IsApproved
		/// </summary>
		public bool IsApproved { get; set; }

		/// <summary>
		/// AuthorUserAgent
		/// </summary>
		public string AuthorUserAgent { get; set; } = string.Empty;

		/// <summary>
		/// Type
		/// </summary>
		public CommentType Type { get; set; } = CommentType.Blank;

		/// <summary>
		/// ParentId
		/// </summary>
		public long ParentId { get; set; }

		/// <summary>
		/// AuthorId
		/// </summary>
		public long AuthorId { get; set; }

		/// <summary>
		/// AuthorIsSubscribed
		/// </summary>
		public bool AuthorIsSubscribed { get; set; }
	}
}
