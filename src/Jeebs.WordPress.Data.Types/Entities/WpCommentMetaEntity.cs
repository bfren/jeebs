// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// CommentMeta entity
	/// </summary>
	public abstract record class WpCommentMetaEntity : WpCommentMetaEntityWithId
	{
		/// <summary>
		/// CommentId
		/// </summary>
		public WpCommentId CommentId { get; init; } = new();

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
