// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Common.UnitOfWork_Tests;

public class RollbackAsync_Tests : UnitOfWork_Setup
{
	[Fact]
	public async Task Begun__Rolls_Back__Logs()
	{
		// Arrange
		var (unitOfWork, v) = Setup();

		// Act
		await unitOfWork.RollbackAsync();

		// Assert
		await v.Transaction.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
		v.Log.Received().Dbg("Rolling back transaction.");
	}

	[Fact]
	public async Task Not_Begun__Does_Not_Begin_Transaction__Does_Not_Log()
	{
		// Arrange
		var (unitOfWork, v) = Setup(begun: false);

		// Act
		await unitOfWork.RollbackAsync();

		// Assert
		Assert.False(unitOfWork.Transaction.Initialised);
		v.Log.DidNotReceive().Dbg("Rolling back transaction.");
	}

	[Fact]
	public async Task Not_Begun__Then_Write__Dispose_Rolls_Back_The_Write()
	{
		// Arrange
		var (unitOfWork, v) = Setup(begun: false);
		await unitOfWork.RollbackAsync();
		_ = await unitOfWork.Transaction.Value; // a write begins the transaction

		// Act
		await unitOfWork.DisposeAsync();

		// Assert
		await v.Transaction.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task Rolls_Back_Only_Once()
	{
		// Arrange
		var (unitOfWork, v) = Setup();

		// Act
		await unitOfWork.RollbackAsync();
		await unitOfWork.RollbackAsync();
		await unitOfWork.RollbackAsync();

		// Assert
		await v.Transaction.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task After_Commit__Does_Not_Roll_Back()
	{
		// Arrange
		var (unitOfWork, v) = Setup();
		_ = await unitOfWork.CommitAsync();

		// Act
		await unitOfWork.RollbackAsync();

		// Assert
		await v.Transaction.DidNotReceive().RollbackAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task Throws__Logs_Error()
	{
		// Arrange
		var (unitOfWork, v) = Setup();
		var exception = new InvalidOperationException(Rnd.Str);
		v.Transaction.When(t => t.RollbackAsync(Arg.Any<CancellationToken>())).Throw(exception);

		// Act
		await unitOfWork.RollbackAsync();

		// Assert
		v.Log.Received().Err(exception, "Error rolling back transaction.");
	}
}
