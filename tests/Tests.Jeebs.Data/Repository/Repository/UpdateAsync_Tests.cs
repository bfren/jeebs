// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Repository.Repository_Tests;

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
		await repo.UpdateAsync(model);

		// Assert
		v.Client.Received().GetUpdateQuery<Foo, FooModel>(value);
	}

	[Fact]
	public async Task Calls_Db_ExecuteAsync()
	{
		// Arrange
		var (repo, v) = Setup();
		var model = new FooModel { Id = IdGen.LongId<FooId>() };

		// Act
		await repo.UpdateAsync(model);

		// Assert
		await v.Db.Received().ExecuteAsync(Arg.Any<string>(), model);
	}

	[Fact]
	public async Task Logs_Query_To_Verbose()
	{
		// Arrange
		var (repo, v) = Setup();
		var value = Rnd.Lng;
		var model = new FooModel { Id = FooId.Wrap(value) };

		// Act
		await repo.UpdateAsync(model);

		// Assert
		v.Log.ReceivedWithAnyArgs().Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
