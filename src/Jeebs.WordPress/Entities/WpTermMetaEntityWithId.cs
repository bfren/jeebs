// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Entities.Ids;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// TermMeta entity.
/// </summary>
public abstract record class WpTermMetaEntityWithId : WithId<WpTermMetaId, ulong>;
