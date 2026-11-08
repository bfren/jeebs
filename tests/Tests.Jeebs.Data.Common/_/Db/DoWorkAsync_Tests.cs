// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Common.Db_Tests;

public class DoWorkAsync_Tests : Db_Setup
{
	[Fact]
	public async Task Work_Succeeds__Commits__Returns_Value()
	{
		// Arrange
		var (db, v) = Setup();
		var value = Rnd.Int;
		v.UnitOfWork.CommitAsync(Arg.Any<int>()).Returns(x => R.Wrap(x.Arg<int>()));

		// Act
		var result = await db.DoWorkAsync(_ => Task.FromResult(R.Wrap(value)));

		// Assert
		Assert.Equal(value, result.AssertOk());
		await v.UnitOfWork.Received(1).CommitAsync(value);
		await v.UnitOfWork.Received(1).DisposeAsync();
	}

	[Fact]
	public async Task Work_Fails__Does_Not_Commit__Returns_Failure()
	{
		// Arrange
		var (db, v) = Setup();
		var message = Rnd.Str;

		// Act
		var result = await db.DoWorkAsync(_ => Task.FromResult<Result<int>>(R.Fail(message)));

		// Assert
		result.AssertFailure(message);
		await v.UnitOfWork.DidNotReceiveWithAnyArgs().CommitAsync(default(int));
		await v.UnitOfWork.Received(1).DisposeAsync();
	}

	[Fact]
	public async Task Work_Throws__Does_Not_Commit__Disposes()
	{
		// Arrange
		var (db, v) = Setup();

		// Act
		var action = () => db.DoWorkAsync<int>(_ => throw new InvalidOperationException());

		// Assert
		await Assert.ThrowsAsync<InvalidOperationException>(action);
		await v.UnitOfWork.DidNotReceiveWithAnyArgs().CommitAsync(default(int));
		await v.UnitOfWork.Received(1).DisposeAsync();
	}
}
