// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// CommentMeta entity
	/// </summary>
	public abstract record WpCommentMetaEntity : IWithId<WpCommentMetaId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpCommentMetaId Id
		{
			get =>
				new(CommentMetaId);

			init =>
				CommentMetaId = value.Value;
		}

		/// <summary>
		/// CommentMetaId
		/// </summary>
		[Id]
		public long CommentMetaId { get; init; }

		/// <summary>
		/// CommentId
		/// </summary>
		public long CommentId { get; init; }

		/// <summary>
		/// Key
		/// </summary>
		public string? Key { get; init; }

		/// <summary>
		/// Value
		/// </summary>
		public string? Value { get; init; }
	}
}
