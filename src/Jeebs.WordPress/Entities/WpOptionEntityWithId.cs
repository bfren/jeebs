// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Entities.Ids;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Option entity with ID properties.
/// </summary>
public abstract record class WpOptionEntityWithId : WithId<WpOptionId, ulong>;
