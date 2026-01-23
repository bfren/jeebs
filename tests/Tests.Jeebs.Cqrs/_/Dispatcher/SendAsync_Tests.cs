// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.Cqrs.Dispatcher_Tests;

public class SendAsync_Tests
{
	public class With_Command
	{
		[Fact]
		public async Task Unregistered_Handler_Returns_Fail()
		{
			// Arrange
			var provider = Substitute.For<IServiceProvider>();
			var log = Substitute.For<ILog<Dispatcher>>();
			var dispatcher = new Dispatcher(provider, log);
			var command = new TestCommand();

			// Act
			var r0 = await dispatcher.SendAsync(command);
			var r1 = await dispatcher.SendAsync(command);

			// Assert
			_ = r0.AssertFail("Unable to get command handler {Type}.", new { Type = nameof(TestCommand) });
			_ = r1.AssertFail("Unable to get command handler {Type}.", new { Type = nameof(TestCommand) });
		}

		[Fact]
		public async Task Runs_CommandHandler_HandleAsync()
		{
			// Arrange
			var handler = Substitute.For<CommandHandler<TestCommand>>();
			var provider = Substitute.For<IServiceProvider>();
			provider.GetService(default!)
				.ReturnsForAnyArgs(handler);
			var log = Substitute.For<ILog<Dispatcher>>();
			var dispatcher = new Dispatcher(provider, log);
			var command = new TestCommand();

			// Act
			await dispatcher.SendAsync(command);
			await dispatcher.SendAsync(command);
			await dispatcher.SendAsync<TestCommand>();

			// Assert
			await handler.Received(3).HandleAsync(command);
		}
	}

	public class With_Query
	{
		[Fact]
		public async Task Unregistered_Handler_Returns_Fail()
		{
			// Arrange
			var provider = Substitute.For<IServiceProvider>();
			var log = Substitute.For<ILog<Dispatcher>>();
			var dispatcher = new Dispatcher(provider, log);
			var query = new TestQuery();

			// Act
			var r0 = await dispatcher.SendAsync(query);
			var r1 = await dispatcher.SendAsync(query);

			// Assert
			_ = r0.AssertFail("Unable to get query handler {Type}.", new { Type = nameof(TestQuery) });
			_ = r1.AssertFail("Unable to get query handler {Type}.", new { Type = nameof(TestQuery) });
		}

		[Fact]
		public async Task Runs_QueryHandler_HandleAsync()
		{
			// Arrange
			var handler = Substitute.For<QueryHandler<TestQuery, bool>>();
			var provider = Substitute.For<IServiceProvider>();
			provider.GetService(default!)
				.ReturnsForAnyArgs(handler);
			var log = Substitute.For<ILog<Dispatcher>>();
			var dispatcher = new Dispatcher(provider, log);
			var query = new TestQuery();

			// Act
			await dispatcher.SendAsync(query);
			await dispatcher.SendAsync(query);

			// Assert
			await handler.Received(2).HandleAsync(query);
		}
	}

	public sealed record class TestCommand : Command;

	public sealed record class TestQuery : Query<bool>;
}
