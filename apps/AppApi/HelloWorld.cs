// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cqrs;
using Maybe;
using Maybe.Functions;

namespace AppApi;

public class SayHelloHandler : IQueryHandler<SayHelloQuery, string>
{
	public Task<Maybe<string>> HandleAsync(SayHelloQuery query, CancellationToken cancellationToken) =>
		MaybeF.Some($"Hello, {query.Name}!").AsTask;
}

public record struct SayHelloQuery(string Name);
