// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Repository.Repository_Tests;

public class DeleteAsync_Tests : Repository_Setup
{
	[Fact]
	public async Task Calls_Client_GetDeleteQuery()
	{
		// Arrange
		var (repo, v) = Setup();
		var value = Rnd.Lng;
		var model = new FooModel { Id = FooId.Wrap(value) };

		// Act
		await repo.DeleteAsync(model);

		// Assert
		v.Client.Received().GetDeleteQuery<Foo>(value);
	}

	[Fact]
	public async Task Calls_Db_ExecuteAsync()
	{
		// Arrange
		var (repo, v) = Setup();
		var model = new Foo { Id = IdGen.LongId<FooId>() };

		// Act
		_ = await repo.DeleteAsync(model);

		// Assert
		await v.Db.Received(1).ExecuteAsync(Arg.Any<string>(), model);
	}

	[Fact]
	public async Task Logs_Query_To_Verbose()
	{
		// Arrange
		var (repo, v) = Setup();
		var value = Rnd.Lng;
		var model = new FooModel { Id = FooId.Wrap(value) };

		// Act
		await repo.DeleteAsync(model);

		// Assert
		v.Log.ReceivedWithAnyArgs().Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
