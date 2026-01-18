// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.WordPress.Entities.StrongIds;
using StrongId;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// PostMeta entity.
/// </summary>
public abstract record class WpPostMetaEntityWithId : IWithId<WpPostMetaId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpPostMetaId Id { get; init; } = new();
}
