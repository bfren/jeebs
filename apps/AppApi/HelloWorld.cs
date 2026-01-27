// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cqrs;
using Wrap;

namespace AppApi;

public class SayHelloHandler : QueryHandler<SayHelloQuery, string>
{
	public override Task<Result<string>> HandleAsync(SayHelloQuery query) =>
		R.Wrap($"Hello, {query.Name}!").AsTask();
}

public sealed record class SayHelloQuery(string Name) : Query<string>;
