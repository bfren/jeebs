// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Repository_Tests;

public class RetrieveAsync_Tests
{
	[Fact]
	public async Task Calls_Client_GetRetrieveQuery()
	{
		// Arrange
		var (client, _, repo) = Repository_Setup.Get();
		var value = Rnd.Lng;

		// Act
		await repo.RetrieveAsync<Repository_Setup.FooModel>(new Repository_Setup.FooId() { Value = value }).ConfigureAwait(false);

		// Assert
		client.Received().GetRetrieveQuery<Repository_Setup.Foo, Repository_Setup.FooModel>(value);
	}

	[Fact]
	public async Task Logs_Query_To_Debug()
	{
		// Arrange
		var (_, log, repo) = Repository_Setup.Get();
		var value = Rnd.Lng;

		// Act
		await repo.RetrieveAsync<Repository_Setup.FooModel>(new Repository_Setup.FooId() { Value = value }).ConfigureAwait(false);

		// Assert
		log.ReceivedWithAnyArgs().Dbg(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
