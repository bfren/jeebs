// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cqrs.Internals;
using Jeebs.Cqrs.Messages;

namespace Jeebs.Cqrs.CommandHandler_Tests;

public class HandleAsync_Tests
{
	[Fact]
	public async Task Without_CancellationToken_Calls_HandleAsync_With_CancellationToken_None()
	{
		// Arrange
		var handler = Substitute.For<CommandHandler<Command>>();
		var command = new Command();

		// Act
		await handler.HandleAsync(command);

		// Assert
		await handler.Received().HandleAsync(command, CancellationToken.None);
	}

	[Fact]
	public async Task As_Interface_With_Incorrect_Command_Type_Returns_None_With_IncorrectCommandTypeMsg()
	{
		// Arrange
		var handler = Substitute.For<CommandHandler<Command>>();
		var command = new IncorrectCommand();

		// Act
		var result = await ((ICommandHandler)handler).HandleAsync(command, CancellationToken.None);

		// Assert
		var none = result.AssertNone();
		var msg = Assert.IsType<IncorrectCommandTypeMsg>(none);
		Assert.Equal(typeof(Command), msg.ExpectedType);
		Assert.Equal(typeof(IncorrectCommand), msg.ActualType);
	}

	[Fact]
	public async Task As_Interface_With_Correct_Command_Type_Calls_HandleAsync()
	{
		// Arrange
		var handler = Substitute.For<CommandHandler<Command>>();
		var command = new Command();

		// Act
		await ((ICommandHandler)handler).HandleAsync(command, CancellationToken.None);

		// Assert
		await handler.Received().HandleAsync(command, CancellationToken.None);
	}

	public sealed record class Command : ICommand;

	public sealed record class IncorrectCommand : ICommand;
}
