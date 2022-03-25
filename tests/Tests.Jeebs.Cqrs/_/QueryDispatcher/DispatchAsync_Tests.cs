// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cqrs.Messages;
using Jeebs.Logging;

namespace Jeebs.Cqrs.QueryDispatcher_Tests;

public class DispatchAsync_Tests
{
	[Fact]
	public async Task Calls_Log_Dbg()
	{
		// Arrange
		var provider = Substitute.For<IServiceProvider>();
		var log = Substitute.For<ILog<QueryDispatcher>>();
		var dispatcher = new QueryDispatcher(provider, log);
		var query = new Query();

		// Act
		await dispatcher.DispatchAsync(query).ConfigureAwait(false);
		await dispatcher.DispatchAsync(query, CancellationToken.None).ConfigureAwait(false);

		// Assert
		log.Received(2).Vrb("Query handler type: {Type}", typeof(QueryHandler<Query, bool>));
	}

	[Fact]
	public async Task Unregistered_Handler_Returns_None_With_UnableToGetQueryHandlerMsg()
	{
		// Arrange
		var provider = Substitute.For<IServiceProvider>();
		var log = Substitute.For<ILog<QueryDispatcher>>();
		var dispatcher = new QueryDispatcher(provider, log);
		var query = new Query();

		// Act
		var r0 = await dispatcher.DispatchAsync(query).ConfigureAwait(false);
		var r1 = await dispatcher.DispatchAsync(query, CancellationToken.None).ConfigureAwait(false);

		// Assert
		var n0 = r0.AssertNone();
		var m0 = Assert.IsAssignableFrom<UnableToGetQueryHandlerMsg>(n0);
		Assert.Equal(typeof(Query), m0.Value);
		var n1 = r1.AssertNone();
		var m1 = Assert.IsAssignableFrom<UnableToGetQueryHandlerMsg>(n1);
		Assert.Equal(typeof(Query), m1.Value);
	}

	[Fact]
	public async Task Runs_QueryHandler_HandleAsync()
	{
		// Arrange
		var handler = Substitute.For<QueryHandler<Query, bool>>();
		var provider = Substitute.For<IServiceProvider>();
		provider.GetService(default!)
			.ReturnsForAnyArgs(handler);
		var log = Substitute.For<ILog<QueryDispatcher>>();
		var dispatcher = new QueryDispatcher(provider, log);
		var query = new Query();

		// Act
		await dispatcher.DispatchAsync(query).ConfigureAwait(false);
		await dispatcher.DispatchAsync(query, CancellationToken.None).ConfigureAwait(false);

		// Assert
		await handler.Received(2).HandleAsync(query, CancellationToken.None);
	}

	public sealed record class Query : IQuery<bool>;
}
