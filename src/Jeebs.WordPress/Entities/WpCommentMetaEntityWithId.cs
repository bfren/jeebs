// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities;

/// <summary>
/// CommentMeta entity with ID properties
/// </summary>
public abstract record class WpCommentMetaEntityWithId : Id.IWithId<WpCommentMetaId>
{
	/// <summary>
	/// Id
	/// </summary>
	public WpCommentMetaId Id { get; init; } = new();
}
