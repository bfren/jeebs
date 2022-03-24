// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Repository_Tests;

public class CreateAsync_Tests
{
	[Fact]
	public async Task Calls_Client_GetCreateQuery()
	{
		// Arrange
		var (client, _, repo) = Repository_Setup.Get();
		var foo = new Repository_Setup.Foo { Id = new(Rnd.Lng) };

		// Act
		await repo.CreateAsync(foo).ConfigureAwait(false);

		// Assert
		client.Received().GetCreateQuery<Repository_Setup.Foo>();
	}

	[Fact]
	public async Task Logs_Query_To_Debug()
	{
		// Arrange
		var (_, log, repo) = Repository_Setup.Get();
		var foo = new Repository_Setup.Foo { Id = new(Rnd.Lng) };

		// Act
		await repo.CreateAsync(foo).ConfigureAwait(false);

		// Assert
		log.ReceivedWithAnyArgs().Dbg(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
