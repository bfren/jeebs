// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities;

/// <summary>
/// TermMeta entity
/// </summary>
public abstract record class WpTermMetaEntityWithId : Id.IWithId<WpTermMetaId>
{
	/// <summary>
	/// Id
	/// </summary>
	[Id]
	public WpTermMetaId Id { get; init; } = new();
}
