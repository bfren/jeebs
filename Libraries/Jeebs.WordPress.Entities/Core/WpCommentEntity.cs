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
		public int Id { get => CommentId; set => CommentId = value; }

		/// <summary>
		/// CommentId
		/// </summary>
		[Id]
		public int CommentId { get; set; }

		/// <summary>
		/// PostId
		/// </summary>
		public int PostId { get; set; }

		/// <summary>
		/// AuthorName
		/// </summary>
		public string AuthorName { get; set; }

		/// <summary>
		/// AuthorEmail
		/// </summary>
		public string AuthorEmail { get; set; }

		/// <summary>
		/// AuthorUrl
		/// </summary>
		public string AuthorUrl { get; set; }

		/// <summary>
		/// AuthorIp
		/// </summary>
		public string AuthorIp { get; set; }

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
		/// Karma
		/// </summary>
		public int Karma { get; set; }

		/// <summary>
		/// IsApproved
		/// </summary>
		public bool IsApproved { get; set; }

		/// <summary>
		/// AuthorUserAgent
		/// </summary>
		public string AuthorUserAgent { get; set; }

		/// <summary>
		/// Type
		/// </summary>
		public CommentType Type { get; set; }

		/// <summary>
		/// ParentId
		/// </summary>
		public int ParentId { get; set; }

		/// <summary>
		/// AuthorId
		/// </summary>
		public int AuthorId { get; set; }

		/// <summary>
		/// AuthorIsSubscribed
		/// </summary>
		public bool AuthorIsSubscribed { get; set; }

		/// <summary>
		/// Create object
		/// </summary>
		protected WpCommentEntity()
		{
			AuthorName = string.Empty;
			AuthorEmail = string.Empty;
			AuthorUrl = string.Empty;
			AuthorIp = string.Empty;
			Content = string.Empty;
			AuthorUserAgent = string.Empty;
			Type = CommentType.Blank;
		}
	}
}
