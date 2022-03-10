// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// User entity
/// </summary>
public abstract record class WpUserEntityWithId : Id.IWithId<WpUserId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpUserId Id { get; init; } = new();
}
