// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.WordPress.Entities.StrongIds;
using StrongId;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Option entity with ID properties.
/// </summary>
public abstract record class WpOptionEntityWithId : IWithId<WpOptionId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpOptionId Id { get; init; } = new();
}
