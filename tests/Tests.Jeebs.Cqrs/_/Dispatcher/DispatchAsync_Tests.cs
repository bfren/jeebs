// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cqrs.Messages;
using Jeebs.Logging;

namespace Jeebs.Cqrs.Dispatcher_Tests;

public class DispatchAsync_Tests
{
	public class With_Command
	{
		[Fact]
		public async Task Unregistered_Handler_Returns_None_With_UnableToGetCommandHandlerMsg()
		{
			// Arrange
			var provider = Substitute.For<IServiceProvider>();
			var log = Substitute.For<ILog<Dispatcher>>();
			var dispatcher = new Dispatcher(provider, log);
			var command = new Command();

			// Act
			var r0 = await dispatcher.DispatchAsync(command);
			var r1 = await dispatcher.DispatchAsync(command);

			// Assert
			var n0 = r0.AssertNone();
			var m0 = Assert.IsAssignableFrom<UnableToGetCommandHandlerMsg>(n0);
			Assert.Equal(typeof(Command), m0.Value);
			var n1 = r1.AssertNone();
			var m1 = Assert.IsAssignableFrom<UnableToGetCommandHandlerMsg>(n1);
			Assert.Equal(typeof(Command), m1.Value);
		}

		[Fact]
		public async Task Runs_CommandHandler_HandleAsync()
		{
			// Arrange
			var handler = Substitute.For<CommandHandler<Command>>();
			var provider = Substitute.For<IServiceProvider>();
			provider.GetService(default!)
				.ReturnsForAnyArgs(handler);
			var log = Substitute.For<ILog<Dispatcher>>();
			var dispatcher = new Dispatcher(provider, log);
			var command = new Command();

			// Act
			await dispatcher.DispatchAsync(command);
			await dispatcher.DispatchAsync(command);

			// Assert
			await handler.Received(2).HandleAsync(command);
		}
	}

	public class With_Query
	{
		[Fact]
		public async Task Unregistered_Handler_Returns_None_With_UnableToGetQueryHandlerMsg()
		{
			// Arrange
			var provider = Substitute.For<IServiceProvider>();
			var log = Substitute.For<ILog<Dispatcher>>();
			var dispatcher = new Dispatcher(provider, log);
			var query = new Query();

			// Act
			var r0 = await dispatcher.DispatchAsync(query);
			var r1 = await dispatcher.DispatchAsync(query);

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
			var log = Substitute.For<ILog<Dispatcher>>();
			var dispatcher = new Dispatcher(provider, log);
			var query = new Query();

			// Act
			await dispatcher.DispatchAsync(query);
			await dispatcher.DispatchAsync(query);

			// Assert
			await handler.Received(2).HandleAsync(query);
		}
	}

	public sealed record class Command : Cqrs.Command;

	public sealed record class Query : Query<bool>;
}
