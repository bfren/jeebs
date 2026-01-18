// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Entities.StrongIds;
using StrongId;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Comment entity ID properties.
/// </summary>
public abstract record class WpCommentEntityWithId : IWithId<WpCommentId>
{
	/// <summary>
	/// Id
	/// </summary>
	public WpCommentId Id { get; init; } = new();
}
