// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;
using Jeebs.WordPress.Entities.StrongIds;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// PostMeta entity
/// </summary>
public abstract record class WpPostMetaEntityWithId : Id.IWithId<WpPostMetaId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpPostMetaId Id { get; init; } = new();
}