// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data.Common;
using Jeebs.Logging;

namespace Jeebs.Data.Common.UnitOfWork_Tests;

public class CommitAsync_Tests
{
	private static (UnitOfWork, DbTransaction) Setup()
	{
		var transaction = Substitute.ForPartsOf<DbTransaction>();
		var connection = Substitute.ForPartsOf<DbConnection>();
		var log = Substitute.For<ILog>();
		return (new(connection, transaction, log), transaction);
	}

	[Fact]
	public void Commit_Succeeds__Returns_True()
	{
		// Arrange
		var (unitOfWork, _) = Setup();

		// Act
		var result = unitOfWork.Commit();

		// Assert
		Assert.True(result.AssertOk());
	}

	[Fact]
	public void Commit_Throws__Rolls_Back__Returns_Fail_With_Exception()
	{
		// Arrange
		var (unitOfWork, transaction) = Setup();
		var exception = new InvalidOperationException(Rnd.Str);
		transaction.When(t => t.Commit()).Throw(exception);

		// Act
		var result = unitOfWork.Commit();

		// Assert
		var fail = result.AssertFailure();
		Assert.Same(exception, fail.Exception);
		transaction.Received(1).Rollback();
	}

	[Fact]
	public void Commit_Twice__Returns_Fail()
	{
		// Arrange
		var (unitOfWork, _) = Setup();
		_ = unitOfWork.Commit();

		// Act
		var result = unitOfWork.Commit();

		// Assert
		_ = result.AssertFailure("No pending transaction to commit.");
	}

	[Fact]
	public async Task CommitAsync_Succeeds__Returns_True()
	{
		// Arrange
		var (unitOfWork, transaction) = Setup();

		// Act
		var result = await unitOfWork.CommitAsync();

		// Assert
		Assert.True(result.AssertOk());
		await transaction.Received(1).CommitAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task CommitAsync_Throws__Rolls_Back__Returns_Fail_With_Exception()
	{
		// Arrange
		var (unitOfWork, transaction) = Setup();
		var exception = new InvalidOperationException(Rnd.Str);
		transaction.When(t => t.CommitAsync()).Throw(exception);

		// Act
		var result = await unitOfWork.CommitAsync();

		// Assert
		var fail = result.AssertFailure();
		Assert.Same(exception, fail.Exception);
		await transaction.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task CommitAsync_With_Value__Succeeds__Returns_Value()
	{
		// Arrange
		var (unitOfWork, _) = Setup();
		var value = Rnd.Lng;

		// Act
		var result = await unitOfWork.CommitAsync(value);

		// Assert
		Assert.Equal(value, result.AssertOk());
	}

	[Fact]
	public async Task CommitAsync_With_Value__Throws__Returns_Fail()
	{
		// Arrange
		var (unitOfWork, transaction) = Setup();
		var exception = new InvalidOperationException(Rnd.Str);
		transaction.When(t => t.CommitAsync()).Throw(exception);

		// Act
		var result = await unitOfWork.CommitAsync(Rnd.Lng);

		// Assert
		var fail = result.AssertFailure();
		Assert.Same(exception, fail.Exception);
	}
}
