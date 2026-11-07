// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Common.Db_Tests;

public class ExecuteAsync_Tests : Db_Setup
{
	[Fact]
	public async Task Without_Transaction_Starts_UnitOfWork()
	{
		// Arrange
		var (db, v) = Setup();
		var query = Rnd.Str;
		var param = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.ExecuteAsync(query, param, type);

		// Assert
		await db.Received().StartWorkAsync();
	}

	[Fact]
	public async Task Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (db, v) = Setup();
		var query = Rnd.Str;
		var param = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.ExecuteAsync(query, param);
		await db.ExecuteAsync(query, param, type, v.Transaction);

		// Assert
		v.Log.Received(2).Vrb(
			"Query Type: {Type} | Returns: {Return} | {Query} | Parameters: {Param}",
			type, typeof(bool), query, param
		);
	}

	[Fact]
	public async Task Without_Transaction__Query_Succeeds__Commits_With_Result()
	{
		// Arrange
		var (db, v) = Setup();
		v.Client.Adapter.ExecuteAsync(v.Transaction, Arg.Any<string>(), Arg.Any<object?>(), Arg.Any<CommandType>())
			.Returns(1);
		v.UnitOfWork.CommitAsync(Arg.Any<bool>()).Returns(x => R.Wrap(x.Arg<bool>()));

		// Act
		var result = await db.ExecuteAsync(Rnd.Str, null, CommandType.Text);

		// Assert
		Assert.True(result.AssertOk());
		await v.UnitOfWork.Received(1).CommitAsync(true);
	}

	[Fact]
	public async Task Without_Transaction__Query_Fails__Does_Not_Commit()
	{
		// Arrange
		var (db, v) = Setup();
		var message = Rnd.Str;
		v.Client.Adapter.ExecuteAsync(v.Transaction, Arg.Any<string>(), Arg.Any<object?>(), Arg.Any<CommandType>())
			.Returns(R.Fail(message));

		// Act
		var result = await db.ExecuteAsync(Rnd.Str, null, CommandType.Text);

		// Assert
		_ = result.AssertFailure(message);
		await v.UnitOfWork.DidNotReceiveWithAnyArgs().CommitAsync(default(bool));
	}

	[Fact]
	public async Task Without_Transaction__Commit_Fails__Returns_Fail()
	{
		// Arrange
		var (db, v) = Setup();
		var message = Rnd.Str;
		v.UnitOfWork.CommitAsync(Arg.Any<bool>()).Returns(R.Fail(message));

		// Act
		var result = await db.ExecuteAsync(Rnd.Str, null, CommandType.Text);

		// Assert
		_ = result.AssertFailure(message);
	}
}
