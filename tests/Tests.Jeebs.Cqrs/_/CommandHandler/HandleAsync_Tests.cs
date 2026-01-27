// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cqrs.Internals;

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
		await handler.Received().HandleAsync(command);
	}

	[Fact]
	public async Task As_Interface_With_Incorrect_Command_Type_Returns_None_With_IncorrectCommandTypeMsg()
	{
		// Arrange
		var handler = Substitute.For<CommandHandler<Command>>();
		var command = new IncorrectCommand();

		// Act
		var result = await ((ICommandHandler)handler).HandleAsync(command);

		// Assert
		_ = result.AssertFailure("Incorrect command type.");
	}

	[Fact]
	public async Task As_Interface_With_Correct_Command_Type_Calls_HandleAsync()
	{
		// Arrange
		var handler = Substitute.For<CommandHandler<Command>>();
		var command = new Command();

		// Act
		await ((ICommandHandler)handler).HandleAsync(command);

		// Assert
		await handler.Received().HandleAsync(command);
	}

	public sealed record class Command : Cqrs.Command;

	public sealed record class IncorrectCommand : Cqrs.Command;
}
