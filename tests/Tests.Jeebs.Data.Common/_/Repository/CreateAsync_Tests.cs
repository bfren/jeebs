// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Common.Repository_Tests;

public class CreateAsync_Tests : Repository_Setup
{
	[Fact]
	public async Task Calls_Client_GetCreateQuery()
	{
		// Arrange
		var (repo, v) = Setup();
		var model = new Foo { Id = IdGen.LongId<FooId>() };

		// Act
		_ = await repo.CreateAsync(model);
		_ = await repo.CreateAsync(model, v.Transaction);

		// Assert
		v.Client.Received(2).GetCreateQuery<Foo>();
	}

	[Fact]
	public async Task Calls_Db_ExecuteAsync()
	{
		// Arrange
		var (repo, v) = Setup();
		var model = new Foo { Id = IdGen.LongId<FooId>() };

		// Act
		_ = await repo.CreateAsync(model);
		_ = await repo.CreateAsync(model, v.Transaction);

		// Assert
		await v.Db.Received(2).ExecuteAsync<FooId>(Arg.Any<string>(), model, CommandType.Text, v.Transaction);
	}

	[Fact]
	public async Task Logs_Query_To_Verbose()
	{
		// Arrange
		var (repo, v) = Setup();
		var model = new Foo { Id = IdGen.LongId<FooId>() };

		// Act
		_ = await repo.CreateAsync(model);
		_ = await repo.CreateAsync(model, v.Transaction);

		// Assert
		v.Log.ReceivedWithAnyArgs(2).Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
