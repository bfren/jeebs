// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Link entity with ID properties
/// </summary>
public abstract record class WpLinkEntityWithId : Id.IWithId<WpLinkId>
{
	/// <summary>
	/// Id
	/// </summary>
	public WpLinkId Id { get; init; } = new();
}
