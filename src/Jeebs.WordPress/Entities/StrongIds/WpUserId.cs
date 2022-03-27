// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Entities.StrongIds;

/// <summary>
/// WordPress User ID
/// </summary>
/// <param name="Value">ID Value</param>
public readonly record struct WpUserId(long Value) : Id.IStrongId;
