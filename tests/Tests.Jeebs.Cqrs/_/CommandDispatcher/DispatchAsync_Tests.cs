// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Cqrs.CommandDispatcher_Tests;

public class DispatchAsync_Tests
{
	[Fact]
	public async Task Runs_Command_HandleAsync()
	{
		// Arrange
		var handler = Substitute.For<ICommandHandler<int, string>>();
		var collection = new ServiceCollection();
		_ = collection.AddScoped(_ => handler);
		var provider = collection.BuildServiceProvider();

		ICommandDispatcher dispatcher = new CommandDispatcher(provider);
		var value = Rnd.Int;

		// Act
		_ = await dispatcher.DispatchAsync<int, string>(value).ConfigureAwait(false);
		_ = await dispatcher.DispatchAsync<int, string>(value, CancellationToken.None).ConfigureAwait(false);

		// Assert
		_ = await handler.Received(2).HandleAsync(value, CancellationToken.None).ConfigureAwait(false);
	}
}
