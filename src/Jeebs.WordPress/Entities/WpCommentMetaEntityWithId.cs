// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Entities.StrongIds;
using StrongId;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// CommentMeta entity with ID properties.
/// </summary>
public abstract record class WpCommentMetaEntityWithId : IWithId<WpCommentMetaId>
{
	/// <summary>
	/// Id.
	/// </summary>
	public WpCommentMetaId Id { get; init; } = new();
}
