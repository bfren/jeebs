// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Post entity with ID properties
/// </summary>
public abstract record class WpPostEntityWithId : Id.IWithId<WpPostId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpPostId Id { get; init; } = new();
}
