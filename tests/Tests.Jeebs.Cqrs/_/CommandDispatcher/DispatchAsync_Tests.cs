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
		var handler = Substitute.For<ICommandHandler<Command>>();
		var collection = new ServiceCollection();
		_ = collection.AddScoped(_ => handler);
		var provider = collection.BuildServiceProvider();

		ICommandDispatcher dispatcher = new CommandDispatcher(provider);
		var command = new Command();

		// Act
		_ = await dispatcher.DispatchAsync(command).ConfigureAwait(false);
		_ = await dispatcher.DispatchAsync(command, CancellationToken.None).ConfigureAwait(false);

		// Assert
		_ = await handler.Received(2).HandleAsync(command, CancellationToken.None).ConfigureAwait(false);
	}

	public sealed record class Command : ICommand;
}
