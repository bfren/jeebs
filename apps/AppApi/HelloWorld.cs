// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cqrs;
using MaybeF;

namespace AppApi;

public class SayHelloHandler : QueryHandler<SayHelloQuery, string>
{
	public override Task<Maybe<string>> HandleAsync(SayHelloQuery query, CancellationToken cancellationToken) =>
		F.Some($"Hello, {query.Name}!").AsTask;
}

public record struct SayHelloQuery(string Name) : IQuery<string>;
