// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Common.Repository_Tests;

public class CreateAsync_Tests
{
	[Fact]
	public async Task Calls_Client_GetCreateQuery()
	{
		// Arrange
		var (client, _, repo) = Repository_Setup.Get();
		var foo = new Repository_Setup.Foo { Id = IdGen.LongId<Repository_Setup.FooId>() };

		// Act
		await repo.CreateAsync(foo);

		// Assert
		client.Received().GetCreateQuery<Repository_Setup.Foo>();
	}

	[Fact]
	public async Task Logs_Query_To_Verbose()
	{
		// Arrange
		var (_, log, repo) = Repository_Setup.Get();
		var foo = new Repository_Setup.Foo { Id = IdGen.LongId<Repository_Setup.FooId>() };

		// Act
		await repo.CreateAsync(foo);

		// Assert
		log.ReceivedWithAnyArgs().Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
