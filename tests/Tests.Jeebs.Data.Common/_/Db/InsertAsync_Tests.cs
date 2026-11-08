// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Common.Db_Tests;

public class InsertAsync_Tests : Db_Setup
{
	[Fact]
	public async Task Logs_Query_Info_To_Verbose()
	{
		// Arrange
		var (db, v) = Setup();
		var query = Rnd.Str;
		var param = Rnd.Guid.ToString();
		const CommandType type = CommandType.Text;

		// Act
		await db.InsertAsync<int>(query, param, type);
		await db.InsertAsync<int>(query, param, type, v.UnitOfWork);

		// Assert
		v.Log.Received(2).Vrb(
			"Query Type: {Type} | Returns: {Return} | {Query} | Parameters: {Param}",
			type, typeof(int), query, param
		);
	}

	[Fact]
	public async Task Without_Transaction__Query_Succeeds__Commits_With_Result()
	{
		// Arrange
		var (db, v) = Setup();
		var id = Rnd.Int;
		v.Client.Adapter.ExecuteAsync<int>(v.Connection, v.Transaction, Arg.Any<string>(), Arg.Any<object?>(), Arg.Any<CommandType>())
			.Returns(id);
		v.UnitOfWork.CommitAsync(Arg.Any<int>()).Returns(x => R.Wrap(x.Arg<int>()));

		// Act
		var result = await db.InsertAsync<int>(Rnd.Str, null, CommandType.Text);

		// Assert
		Assert.Equal(id, result.AssertOk());
		await v.UnitOfWork.Received(1).CommitAsync(id);
	}

	[Fact]
	public async Task Without_Transaction__Query_Fails__Does_Not_Commit()
	{
		// Arrange
		var (db, v) = Setup();
		var message = Rnd.Str;
		v.Client.Adapter.ExecuteAsync<int>(v.Connection, v.Transaction, Arg.Any<string>(), Arg.Any<object?>(), Arg.Any<CommandType>())
			.Returns(R.Fail(message));

		// Act
		var result = await db.InsertAsync<int>(Rnd.Str, null, CommandType.Text);

		// Assert
		_ = result.AssertFailure(message);
		await v.UnitOfWork.DidNotReceiveWithAnyArgs().CommitAsync(default(int));
	}

	[Fact]
	public async Task Without_Transaction__Commit_Fails__Returns_Fail()
	{
		// Arrange
		var (db, v) = Setup();
		var message = Rnd.Str;
		v.UnitOfWork.CommitAsync(Arg.Any<int>()).Returns(R.Fail(message));

		// Act
		var result = await db.InsertAsync<int>(Rnd.Str, null, CommandType.Text);

		// Assert
		_ = result.AssertFailure(message);
	}
}
