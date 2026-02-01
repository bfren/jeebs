// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Repository.Repository_Tests;

public class RetrieveAsync_Tests : Repository_Setup
{
	[Fact]
	public async Task Calls_Client_GetRetrieveQuery()
	{
		// Arrange
		var (repo, v) = Setup();
		var value = Rnd.Lng;

		// Act
		await repo.RetrieveAsync<FooModel>(FooId.Wrap(value));

		// Assert
		v.Client.Received().GetRetrieveQuery<Foo, FooModel>(value);
	}

	[Fact]
	public async Task Calls_Db_QuerySingleAsync()
	{
		// Arrange
		var (repo, v) = Setup();
		var value = IdGen.LongId<FooId>();

		// Act
		await repo.RetrieveAsync<FooModel>(value);

		// Assert
		await v.Db.Received().QuerySingleAsync<FooModel>(Arg.Any<string>(), Arg.Any<object?>());
	}

	[Fact]
	public async Task Logs_Query_To_Verbose()
	{
		// Arrange
		var (repo, v) = Setup();
		var value = Rnd.Lng;

		// Act
		await repo.RetrieveAsync<FooModel>(FooId.Wrap(value));

		// Assert
		v.Log.ReceivedWithAnyArgs().Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
