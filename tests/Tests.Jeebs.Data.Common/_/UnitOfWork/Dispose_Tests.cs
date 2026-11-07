// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data.Common;
using Jeebs.Logging;

namespace Jeebs.Data.Common.UnitOfWork_Tests;

public class Dispose_Tests
{
	private static (UnitOfWork, DbTransaction) Setup()
	{
		var transaction = Substitute.ForPartsOf<DbTransaction>();
		var connection = Substitute.ForPartsOf<DbConnection>();
		var log = Substitute.For<ILog>();
		return (new(connection, transaction, log), transaction);
	}

	[Fact]
	public void Not_Committed__Calls_Rollback__Does_Not_Commit()
	{
		// Arrange
		var (unitOfWork, transaction) = Setup();

		// Act
		unitOfWork.Dispose();

		// Assert
		transaction.Received(1).Rollback();
		transaction.DidNotReceive().Commit();
	}

	[Fact]
	public void Committed__Does_Not_Rollback()
	{
		// Arrange
		var (unitOfWork, transaction) = Setup();
		_ = unitOfWork.Commit();

		// Act
		unitOfWork.Dispose();

		// Assert
		transaction.DidNotReceive().Rollback();
	}

	[Fact]
	public async Task Async_Not_Committed__Calls_RollbackAsync__Does_Not_Commit()
	{
		// Arrange
		var (unitOfWork, transaction) = Setup();

		// Act
		await unitOfWork.DisposeAsync();

		// Assert
		await transaction.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
		await transaction.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task Async_Committed__Does_Not_Rollback()
	{
		// Arrange
		var (unitOfWork, transaction) = Setup();
		_ = await unitOfWork.CommitAsync();

		// Act
		await unitOfWork.DisposeAsync();

		// Assert
		await transaction.DidNotReceive().RollbackAsync(Arg.Any<CancellationToken>());
	}
}
