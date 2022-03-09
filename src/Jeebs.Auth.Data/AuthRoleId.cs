// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.StrongId;

namespace Jeebs.Auth.Data;

/// <summary>
/// Role ID
/// </summary>
/// <param name="Value">ID Value</param>
public readonly record struct AuthRoleId(long Value) : IStrongId;
