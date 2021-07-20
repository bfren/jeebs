// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// CommentMeta entity with ID properties
	/// </summary>
	public abstract record WpCommentMetaEntityWithId : IWithId<WpCommentMetaId>
	{
		/// <summary>
		/// Id
		/// </summary>
		public WpCommentMetaId Id { get; init; } = new();
	}
}
