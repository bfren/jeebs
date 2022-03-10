// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// UserMeta entity
/// </summary>
public abstract record class WpUserMetaEntityWithId : Id.IWithId<WpUserMetaId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpUserMetaId Id { get; init; } = new();
}
