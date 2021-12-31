// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Cqrs;
using static F.OptionF;

namespace AppApi;

public class SayHelloHandler : IQueryHandler<SayHelloQuery, string>
{
	public Task<Option<string>> HandleAsync(SayHelloQuery query, CancellationToken cancellationToken) =>
		Some($"Hello, {query.Name}!").AsTask;
}

public record struct SayHelloQuery(string Name);
