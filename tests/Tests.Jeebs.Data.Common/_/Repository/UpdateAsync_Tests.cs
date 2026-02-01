// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Common.Repository_Tests;

public class UpdateAsync_Tests : Repository_Setup
{
	[Fact]
	public async Task Calls_Client_GetUpdateQuery()
	{
		// Arrange
		var (repo, v) = Setup();
		var value = Rnd.Lng;
		var model = new FooModel { Id = FooId.Wrap(value) };

		// Act
		_ = await repo.UpdateAsync(model);
		_ = await repo.UpdateAsync(model, v.Transaction);

		// Assert
		v.Client.Received(2).GetUpdateQuery<Foo, FooModel>(value);
	}

	[Fact]
	public async Task Calls_Db_ExecuteAsync()
	{
		// Arrange
		var (repo, v) = Setup();
		var model = new FooModel { Id = IdGen.LongId<FooId>() };

		// Act
		await repo.UpdateAsync(model);
		await repo.UpdateAsync(model, v.Transaction);

		// Assert
		await v.Db.Received(2).ExecuteAsync(Arg.Any<string>(), model, CommandType.Text, v.Transaction);
	}

	[Fact]
	public async Task Logs_Query_To_Verbose()
	{
		// Arrange
		var (repo, v) = Setup();
		var value = Rnd.Lng;
		var model = new FooModel { Id = FooId.Wrap(value) };

		// Act
		_ = await repo.UpdateAsync(model);
		_ = await repo.UpdateAsync(model, v.Transaction);

		// Assert
		v.Log.ReceivedWithAnyArgs(2).Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
