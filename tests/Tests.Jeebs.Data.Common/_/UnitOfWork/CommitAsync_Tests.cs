// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Common.UnitOfWork_Tests;

public class CommitAsync_Tests : UnitOfWork_Setup
{
	[Fact]
	public async Task Begun__Commits__Logs__Returns_True()
	{
		// Arrange
		var (unitOfWork, v) = Setup();

		// Act
		var result = await unitOfWork.CommitAsync();

		// Assert
		Assert.True(result.AssertOk());
		await v.Transaction.Received(1).CommitAsync(Arg.Any<CancellationToken>());
		v.Log.Received().Vrb("Committing transaction.");
	}

	[Fact]
	public async Task Not_Begun__Returns_True__Does_Not_Begin_Transaction()
	{
		// Arrange
		var (unitOfWork, v) = Setup(begun: false);

		// Act
		var result = await unitOfWork.CommitAsync();

		// Assert
		Assert.True(result.AssertOk());
		Assert.False(unitOfWork.Transaction.Initialised);
		await v.Transaction.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task Throws__Rolls_Back__Logs_Error__Returns_Fail_With_Exception()
	{
		// Arrange
		var (unitOfWork, v) = Setup();
		var exception = new InvalidOperationException(Rnd.Str);
		v.Transaction.When(t => t.CommitAsync(Arg.Any<CancellationToken>())).Throw(exception);

		// Act
		var result = await unitOfWork.CommitAsync();

		// Assert
		var fail = result.AssertFailure();
		Assert.Same(exception, fail.Exception);
		await v.Transaction.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
		v.Log.Received().Err(exception, "Error committing transaction.");
	}

	[Fact]
	public async Task Twice__Commits_Once__Returns_True()
	{
		// Arrange
		var (unitOfWork, v) = Setup();
		_ = await unitOfWork.CommitAsync();

		// Act
		var result = await unitOfWork.CommitAsync();

		// Assert
		Assert.True(result.AssertOk());
		await v.Transaction.Received(1).CommitAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task Not_Begun__Then_Write__Commits_The_Write()
	{
		// Arrange
		var (unitOfWork, v) = Setup(begun: false);
		_ = await unitOfWork.CommitAsync();
		_ = await unitOfWork.Transaction.Value; // a write begins the transaction

		// Act
		var result = await unitOfWork.CommitAsync();

		// Assert
		Assert.True(result.AssertOk());
		await v.Transaction.Received(1).CommitAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task Commit_Failed__Then_Commit__Returns_Fail()
	{
		// Arrange
		var (unitOfWork, v) = Setup();
		v.Transaction.When(t => t.CommitAsync(Arg.Any<CancellationToken>())).Throw(new InvalidOperationException());
		_ = await unitOfWork.CommitAsync();

		// Act
		var result = await unitOfWork.CommitAsync();

		// Assert
		_ = result.AssertFailure("No pending transaction to commit.");
	}

	[Fact]
	public async Task After_Rollback__Does_Not_Commit__Returns_Fail()
	{
		// Arrange
		var (unitOfWork, v) = Setup();
		await unitOfWork.RollbackAsync();

		// Act
		var result = await unitOfWork.CommitAsync();

		// Assert
		_ = result.AssertFailure("No pending transaction to commit.");
		await v.Transaction.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
	}

	[Fact]
	public async Task With_Value__Succeeds__Returns_Value()
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
	public async Task With_Value__Throws__Returns_Fail()
	{
		// Arrange
		var (unitOfWork, v) = Setup();
		var exception = new InvalidOperationException(Rnd.Str);
		v.Transaction.When(t => t.CommitAsync(Arg.Any<CancellationToken>())).Throw(exception);

		// Act
		var result = await unitOfWork.CommitAsync(Rnd.Lng);

		// Assert
		var fail = result.AssertFailure();
		Assert.Same(exception, fail.Exception);
	}
}
