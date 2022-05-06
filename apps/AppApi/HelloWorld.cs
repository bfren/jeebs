// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cqrs;
using MaybeF;

namespace AppApi;

public class SayHelloHandler : QueryHandler<SayHelloQuery, string>
{
	public override Task<Maybe<string>> HandleAsync(SayHelloQuery query) =>
		F.Some($"Hello, {query.Name}!").AsTask;
}

public sealed record class SayHelloQuery(string Name) : Query<string>;
