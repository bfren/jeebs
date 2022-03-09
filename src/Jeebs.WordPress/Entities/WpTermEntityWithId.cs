// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Term entity
/// </summary>
public abstract record class WpTermEntityWithId : Id.IWithId<WpTermId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpTermId Id { get; init; } = new();
}
