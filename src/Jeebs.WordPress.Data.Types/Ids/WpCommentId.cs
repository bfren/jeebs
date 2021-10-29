// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Data.Entities;

/// <summary>
/// WordPress Comment ID
/// </summary>
/// <param name="Value">ID Value</param>
public readonly record struct WpCommentId(long Value) : IStrongId;
