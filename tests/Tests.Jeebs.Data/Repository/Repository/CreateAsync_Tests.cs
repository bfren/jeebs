// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Repository.Repository_Tests;

public class CreateAsync_Tests : Repository_Setup
{
	[Fact]
	public async Task Calls_Client_GetCreateQuery()
	{
		// Arrange
		var (repo, v) = Setup();
		var foo = new Foo { Id = IdGen.LongId<FooId>() };

		// Act
		await repo.CreateAsync(foo);

		// Assert
		v.Client.Received().GetCreateQuery<Foo>();
	}

	[Fact]
	public async Task Calls_Db_ExecuteAsync()
	{
		// Arrange
		var (repo, v) = Setup();
		var model = new Foo { Id = IdGen.LongId<FooId>() };

		// Act
		_ = await repo.CreateAsync(model);

		// Assert
		await v.Db.Received().ExecuteAsync<FooId>(Arg.Any<string>(), model);
	}

	[Fact]
	public async Task Logs_Query_To_Verbose()
	{
		// Arrange
		var (repo, v) = Setup();
		var foo = new Foo { Id = IdGen.LongId<FooId>() };

		// Act
		await repo.CreateAsync(foo);

		// Assert
		v.Log.ReceivedWithAnyArgs().Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
