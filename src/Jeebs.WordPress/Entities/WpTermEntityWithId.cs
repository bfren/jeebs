// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Entities.Ids;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Term entity.
/// </summary>
public abstract record class WpTermEntityWithId : WithId<WpTermId, ulong>;
