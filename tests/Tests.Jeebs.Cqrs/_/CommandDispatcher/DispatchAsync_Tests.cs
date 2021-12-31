// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Jeebs.Cqrs.CommandDispatcher_Tests;

public class DispatchAsync_Tests
{
	[Fact]
	public async Task Runs_Command_HandleAsync()
	{
		// Arrange
		var handler = Substitute.For<ICommandHandler<int, string>>();
		var collection = new ServiceCollection();
		collection.AddScoped(_ => handler);
		var provider = collection.BuildServiceProvider();

		ICommandDispatcher dispatcher = new CommandDispatcher(provider);
		var value = F.Rnd.Int;

		// Act
		_ = await dispatcher.DispatchAsync<int, string>(value);
		_ = await dispatcher.DispatchAsync<int, string>(value, CancellationToken.None);

		// Assert
		await handler.Received(2).HandleAsync(value, CancellationToken.None);
	}
}
