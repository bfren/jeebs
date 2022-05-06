// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cqrs;

/// <summary>
/// CQRS query
/// </summary>
/// <typeparam name="TResult">Result value type</typeparam>
public abstract record class Query<TResult>;
