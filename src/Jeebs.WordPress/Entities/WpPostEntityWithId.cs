// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.WordPress.Entities.StrongIds;
using StrongId;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Post entity with ID properties
/// </summary>
public abstract record class WpPostEntityWithId : IWithId<WpPostId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpPostId Id { get; init; } = new();
}
