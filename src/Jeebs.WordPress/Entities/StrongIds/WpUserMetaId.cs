// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities.StrongIds;

/// <summary>
/// WordPress User Meta ID
/// </summary>
/// <param name="Value">ID Value</param>
public readonly record struct WpUserMetaId(long Value) : Id.IStrongId;
