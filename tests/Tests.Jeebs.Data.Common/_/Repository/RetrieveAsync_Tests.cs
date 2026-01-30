// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Common.Repository_Tests;

public class RetrieveAsync_Tests
{
	[Fact]
	public async Task Calls_Client_GetRetrieveQuery()
	{
		// Arrange
		var (client, _, repo) = Repository_Setup.Get();
		var value = Rnd.Lng;

		// Act
		await repo.RetrieveAsync<Repository_Setup.FooModel>(new Repository_Setup.FooId { Value = value });

		// Assert
		client.Received().GetRetrieveQuery<Repository_Setup.Foo, Repository_Setup.FooModel>(value);
	}

	[Fact]
	public async Task Logs_Query_To_Verbose()
	{
		// Arrange
		var (_, log, repo) = Repository_Setup.Get();
		var value = Rnd.Lng;

		// Act
		await repo.RetrieveAsync<Repository_Setup.FooModel>(new Repository_Setup.FooId { Value = value });

		// Assert
		log.ReceivedWithAnyArgs().Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
