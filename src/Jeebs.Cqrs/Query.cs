// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS query.
/// </summary>
/// <typeparam name="T">Result value type.</typeparam>
public abstract record class Query<T>;
