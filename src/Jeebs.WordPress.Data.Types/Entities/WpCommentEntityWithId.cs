// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
		public WpCommentId Id { get; init; } = new();
	}
}
