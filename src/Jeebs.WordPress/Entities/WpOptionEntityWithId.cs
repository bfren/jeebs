// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Option entity with ID properties
/// </summary>
public abstract record class WpOptionEntityWithId : Id.IWithId<WpOptionId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpOptionId Id { get; init; } = new();
}
