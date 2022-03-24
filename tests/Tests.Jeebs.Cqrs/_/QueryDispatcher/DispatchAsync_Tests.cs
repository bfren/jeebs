// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Cqrs.QueryDispatcher_Tests;

public class DispatchAsync_Tests
{
	[Fact]
	public async Task Runs_Command_HandleAsync()
	{
		// Arrange
		var handler = Substitute.For<IQueryHandler<Query, string>>();
		var collection = new ServiceCollection();
		collection.AddScoped(_ => handler);
		var provider = collection.BuildServiceProvider();

		IQueryDispatcher dispatcher = new QueryDispatcher(provider);
		var query = new Query();

		// Act
		await dispatcher.DispatchAsync<Query, string>(query).ConfigureAwait(false);
		await dispatcher.DispatchAsync<Query, string>(query, CancellationToken.None).ConfigureAwait(false);

		// Assert
		await handler.Received(2).HandleAsync(query, CancellationToken.None).ConfigureAwait(false);
	}

	public sealed record class Query : IQuery<string>;
}
