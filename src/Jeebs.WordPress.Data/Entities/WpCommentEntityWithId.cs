// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// Comment entity ID properties
	/// </summary>
	public abstract record WpCommentEntityWithId : IWithId<WpCommentId>
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
		public ulong CommentId { get; init; }
	}
}
