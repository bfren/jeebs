// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data.Common;
using Jeebs.Logging;

namespace Jeebs.Data.Common.UnitOfWork_Tests;

public class DisposeAsync_Tests : UnitOfWork_Setup
{
	[Fact]
	public async Task Not_Committed__Rolls_Back__Disposes_Transaction_And_Connection()
	{
		// Arrange
		var (unitOfWork, v) = Setup();

		// Act
		await unitOfWork.DisposeAsync();

		// Assert
		await v.Transaction.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
		await v.Transaction.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
		await v.Transaction.Received(1).DisposeAsync();
		await v.Connection.Received(1).DisposeAsync();
	}

	[Fact]
	public async Task Committed__Does_Not_Roll_Back()
	{
		// Arrange
		var (unitOfWork, v) = Setup();
		_ = await unitOfWork.CommitAsync();

		// Act
		await unitOfWork.DisposeAsync();

		// Assert
		await v.Transaction.DidNotReceive().RollbackAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task Not_Begun__Does_Not_Begin_Transaction__Disposes_Connection()
	{
		// Arrange
		var (unitOfWork, v) = Setup(begun: false);

		// Act
		await unitOfWork.DisposeAsync();

		// Assert
		Assert.False(unitOfWork.Transaction.Initialised);
		await v.Transaction.DidNotReceive().DisposeAsync();
		await v.Connection.Received(1).DisposeAsync();
	}

	[Fact]
	public async Task Begin_Failed__Logs_Error__Disposes_Connection()
	{
		// Arrange
		var connection = Substitute.ForPartsOf<DbConnection>();
		var log = Substitute.For<ILog>();
		var exception = new InvalidOperationException(Rnd.Str);
		var unitOfWork = new UnitOfWork(connection, new(Task.FromException<DbTransaction>(exception)), log);
		_ = unitOfWork.Transaction.Value;

		// Act
		await unitOfWork.DisposeAsync();

		// Assert
		log.Received().Err(exception, "Error rolling back transaction.");
		await connection.Received(1).DisposeAsync();
	}
}
