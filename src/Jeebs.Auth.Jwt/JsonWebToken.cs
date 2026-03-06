// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Auth.Jwt;

/// <summary>
/// Strongly-typed JSON Web Token wrapper for a string.
/// </summary>
public sealed record class JsonWebToken : Monad<JsonWebToken, string>;
