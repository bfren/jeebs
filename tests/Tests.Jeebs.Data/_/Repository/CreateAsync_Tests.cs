// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static StrongId.Testing.Generator;

namespace Jeebs.Data.Repository_Tests;

public class CreateAsync_Tests
{
	[Fact]
	public async Task Calls_Client_GetCreateQuery()
	{
		// Arrange
		var (client, _, repo) = Repository_Setup.Get();
		var foo = new Repository_Setup.Foo { Id = LongId<Repository_Setup.FooId>() };

		// Act
		await repo.CreateAsync(foo);

		// Assert
		client.Received().GetCreateQuery<Repository_Setup.Foo>();
	}

	[Fact]
	public async Task Logs_Query_To_Debug()
	{
		// Arrange
		var (_, log, repo) = Repository_Setup.Get();
		var foo = new Repository_Setup.Foo { Id = LongId<Repository_Setup.FooId>() };

		// Act
		await repo.CreateAsync(foo);

		// Assert
		log.ReceivedWithAnyArgs().Dbg(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
