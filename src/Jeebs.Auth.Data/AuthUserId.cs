// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.StrongId;

namespace Jeebs.Auth.Data;

/// <summary>
/// User ID
/// </summary>
/// <param name="Value">ID Value</param>
public readonly record struct AuthUserId(long Value) : IStrongId;
