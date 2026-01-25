// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Entities.Ids;

namespace Jeebs.WordPress.Entities;

/// <summary>
/// Comment entity ID properties.
/// </summary>
public abstract record class WpCommentEntityWithId : WithId<WpCommentId, ulong>;
